using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Strategies;
using BusinessLogic.Models;
using BusinessLogic.Models.Suspense;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Suspense.JournalAllocation
{
    public class CombinedTransactionJournalAllocationStrategy : IJournalAllocationStrategy
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISecurityContext _securityContext;
        private readonly ISuspenseJournalService _suspenseJournalService;
        private readonly IJournalAllocationStrategyValidator _journalAllocationStrategyValidator;

        private readonly Guid _processId;

        private readonly string _creditMopCode;
        private readonly string _debitMopCode;
        private readonly string _journalVatCode;
        private readonly string _journalFundCode;

        public CombinedTransactionJournalAllocationStrategy(
            ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext
            , ISuspenseJournalService suspenseJournalService
            , IJournalAllocationStrategyValidator journalAllocationStrategyValidator)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _securityContext = securityContext;
            _suspenseJournalService = suspenseJournalService;
            _journalAllocationStrategyValidator = journalAllocationStrategyValidator;

            _creditMopCode = GetCreditNoteMopCode();
            _debitMopCode = GetCreditNoteMopCode();
            _journalVatCode = GetJournalVatCode();
            _journalFundCode = GetJournalFundCode();

            _processId = Guid.NewGuid();
        }

        private string GetCreditNoteMopCode()
        {
            return _unitOfWork.Mops.GetAll(true).FirstOrDefault(x => x.IsAJournalReallocation()).MopCode;
        }

        private string GetDebitMopCode()
        {
            return _unitOfWork.Mops.GetAll(true).FirstOrDefault(x => x.IsAJournal()).MopCode;
        }

        private string GetJournalVatCode()
        {
            return _unitOfWork.Vats.GetAll(true).FirstOrDefault(x => x.IsASuspenseJournalVatCode()).VatCode;
        }

        private string GetJournalFundCode()
        {
            return _unitOfWork.Funds.GetAll(true).FirstOrDefault(x => x.IsASuspenseJournalFund()).FundCode;
        }

        public IResult Execute(
            List<int> suspenseItems,
            List<Journal> journalItems,
            List<CreditNote> creditNotes)
        {
            try
            {
                Lock(suspenseItems);

                var suspenses = GetSuspenseItems();

                Validate(suspenses, journalItems, creditNotes);

                Allocate(suspenses.First(), journalItems, creditNotes);
            }
            catch (SuspenseJournalAllocationException ex)
            {
                _logger.Error(ex);

                return new Result(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);

                return new Result("Unable to journal the items");
            }
            finally
            {
                try
                {
                    Unlock();
                }
                catch (Exception e)
                {
                    _logger.Error("Unable to unlock suspense records!", e);
                }
            }

            return new Result();
        }

        private void Lock(List<int> suspenseItems)
        {
            _unitOfWork.Suspenses.Lock(suspenseItems, _processId);
            _unitOfWork.Complete(_securityContext.UserId);
        }

        private void Unlock()
        {
            _unitOfWork.Suspenses.Unlock(_processId);
            _unitOfWork.Complete(_securityContext.UserId);
        }

        private List<SuspenseWrapper> GetSuspenseItems()
        {
            return _unitOfWork.Suspenses
                .GetSuspensesBeingProcessed(_processId)
                .Select(x => new SuspenseWrapper(x))
                .OrderByDescending(x => x.Item.Amount)
                .ToList();
        }

        private void Validate(List<SuspenseWrapper> suspenses, List<Journal> journalItems, List<CreditNote> creditNotes)
        {
            _journalAllocationStrategyValidator.Validate(new JournalAllocationStrategyValidatorArgs()
            {
                Suspenses = suspenses,
                JournalItems = journalItems,
                CreditNotes = creditNotes
            });
        }

        private Guid _transferReference = Guid.NewGuid();
        private Guid _transferGuid = Guid.NewGuid();
        private string _pspReference = string.Empty;
        private DateTime _suspenseTransactionDate;

        private void Allocate(
            SuspenseWrapper suspense,
            List<Journal> journalItems,
            List<CreditNote> creditNotes)
        {
            _pspReference = _suspenseJournalService.GetPspReference();
            _suspenseTransactionDate = suspense.Item.CreatedAt;

            var suspenseTransaction = CreateSuspenseTransaction(suspense);

            foreach (var journal in journalItems)
            {
                var journalTransaction = CreateJournalTransaction(journal, suspense);
                CreateSuspenseProcessedTransaction(journal, journalTransaction, suspense, suspenseTransaction);
            }

            CreateCreditNoteJournalTransactions(creditNotes, suspense);
        }

        private ProcessedTransaction CreateSuspenseTransaction(SuspenseWrapper suspense)
        {
            var transferItem = new TransferItem()
            {
                AccountReference = suspense.Item.AccountNumber,
                Amount = -suspense.Item.Amount,
                FundCode = _journalFundCode,
                MopCode = _debitMopCode,
                Narrative = suspense.Item.Narrative,
                ImportId = suspense.Item.ImportId.Value,
                VatCode = _journalVatCode
            };

            return (ProcessedTransaction)_suspenseJournalService.CreateJournal(
                transferItem,
                _transferReference,
                _transferGuid,
                _pspReference,
                _suspenseTransactionDate).Data;
        }

        private ProcessedTransaction CreateJournalTransaction(Journal journal, SuspenseWrapper suspense)
        {
            var transferItem = new TransferItem()
            {
                AccountReference = journal.AccountReference,
                Amount = journal.Amount,
                FundCode = journal.FundCode,
                MopCode = journal.MopCode,
                Narrative = string.IsNullOrWhiteSpace(journal.Narrative) ? suspense.Item.Narrative : journal.Narrative,
                ImportId = suspense.Item.ImportId.Value,
                VatCode = journal.VatCode
            };

            return (ProcessedTransaction)_suspenseJournalService.CreateJournal(
                transferItem,
                _transferReference,
                _transferGuid,
                _pspReference,
                _suspenseTransactionDate).Data;
        }

        private void CreateSuspenseProcessedTransaction(
            Journal journal,
            ProcessedTransaction journalTransaction,
            SuspenseWrapper suspense,
            ProcessedTransaction suspenseTransaction)
        {
            if (journal.Amount >= suspense.AmountRemaining)
            {
                journal.Amount -= suspense.AmountRemaining;

                var a = new SuspenseProcessedTransaction()
                {
                    SuspenseId = suspense.Item.Id,
                    Amount = suspense.AmountRemaining,
                    CreatedAt = DateTime.Now,
                    CreatedByUserId = _securityContext.UserId,
                    TransactionIn = journalTransaction,
                    TransactionOut = suspenseTransaction
                };

                suspense.Item.SuspenseProcessedTransactions.Add(a);
            }
            else if (journal.Amount < suspense.AmountRemaining)
            {
                var a = new SuspenseProcessedTransaction()
                {
                    SuspenseId = suspense.Item.Id,
                    Amount = journal.Amount,
                    CreatedAt = DateTime.Now,
                    CreatedByUserId = _securityContext.UserId,
                    TransactionIn = journalTransaction,
                    TransactionOut = suspenseTransaction
                };

                suspense.Item.SuspenseProcessedTransactions.Add(a);

                journal.Amount = 0;
            }
        }

        private void CreateCreditNoteJournalTransactions(List<CreditNote> creditNotes, SuspenseWrapper suspense)
        {
            if (creditNotes.IsNullOrEmpty()) return;

            foreach (var creditNote in creditNotes)
            {

                var transferItem = new TransferItem()
                {
                    AccountReference = creditNote.AccountReference,
                    Amount = -creditNote.Amount,
                    FundCode = creditNote.FundCode,
                    MopCode = _creditMopCode,
                    Narrative = suspense.Item.Narrative,
                    ImportId = suspense.Item.ImportId.Value,
                    VatCode = creditNote.VatCode
                };

                _ = _suspenseJournalService.CreateJournal(
                    transferItem,
                    _transferReference,
                    _transferGuid,
                    _pspReference,
                    _suspenseTransactionDate).Data;
            }
        }
    }
}
