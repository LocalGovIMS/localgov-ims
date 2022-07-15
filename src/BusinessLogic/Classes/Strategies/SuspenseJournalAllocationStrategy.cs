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

namespace BusinessLogic.Classes.Strategies
{
    public class SuspenseJournalAllocationStrategy : IJournalAllocationStrategy
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISecurityContext _securityContext;
        private readonly ITransactionJournalService _transactionJournalService;
        private readonly IFundService _fundService;

        private readonly Guid _processId;

        private readonly string _creditMopCode;
        private readonly string _journalVatCode;
        private readonly string _journalFundCode;

        public SuspenseJournalAllocationStrategy(
            ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext
            , ITransactionJournalService transactionJournalService
            , IFundService fundService)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _securityContext = securityContext;
            _transactionJournalService = transactionJournalService;
            _fundService = fundService;

            _creditMopCode = GetCreditNoteMopCode();
            _journalVatCode = GetJournalVatCode();
            _journalFundCode = GetJournalFundCode();

            _processId = Guid.NewGuid();
        }

        private string GetCreditNoteMopCode()
        {
            return _unitOfWork.Mops.GetAll(true).FirstOrDefault(x => x.IsAJournalReallocation()).MopCode;
        }

        private string GetJournalVatCode()
        {
            return _unitOfWork.Vats.GetAll(true).FirstOrDefault(x => x.IsASuspenseJournalVatCode()).VatCode;
        }

        private string GetJournalFundCode()
        {
            return _unitOfWork.Funds.GetAll(true).FirstOrDefault(x => x.IsASuspenseJournalFund()).FundCode;
        }

        public IResult Execute(List<int> suspenseItems
            , List<Journal> journalItems
            , List<CreditNote> creditNotes)
        {
            try
            {
                Lock(suspenseItems);

                var suspenses = GetSuspenseItems();

                var validationResult = Validate(suspenses, journalItems, creditNotes);
                if (!validationResult.Success) return validationResult;

                Distribute(suspenses, journalItems, creditNotes);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return new Result.Result("Unable to journal the items");
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

            return new Result.Result();
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

        private IResult Validate(List<SuspenseWrapper> suspenses, List<Journal> journalItems, List<CreditNote> creditNotes)
        {
            if (!suspenses.Any())
            {
                return new Result.Result("You must choose some suspense items");
            }

            if (suspenses.Sum(x => x.AmountRemaining) <= 0)
            {
                return new Result.Result("Value of chosen suspense items must be greater than zero");
            }

            var creditNoteTotal = creditNotes == null || !creditNotes.Any()
                ? 0
                : creditNotes.Sum(c => c.Amount);

            if (journalItems.Sum(x => x.Amount) != (suspenses.Sum(x => x.AmountRemaining) + creditNoteTotal))
            {
                return new Result.Result("Value of chosen suspense items and credit notes must match amount to be journalled");
            }

            if (journalItems.Any(x => x.FundCode == "01"))
            {
                return new Result.Result("You cannot journal a suspense item back to suspense");
            }

            if (creditNotes != null)
            {
                var creditNoteFunds = _fundService.GetCreditNoteFunds().Select(x => x.FundCode).ToList();

                var excludeList = creditNotes.Where(i => creditNoteFunds.Contains(i.FundCode));
                if (excludeList == null) return new Result.Result("Credit notes exist with invalid funds");

                var filteredList = creditNotes.Except(excludeList);
                if (filteredList != null && filteredList.Any())
                {
                    return new Result.Result("Credit notes exist with invalid funds");
                }
            }

            return new Result.Result();
        }

        private void Distribute(List<SuspenseWrapper> suspenses, List<Journal> journalItems, List<CreditNote> creditNotes)
        {
            foreach (var journal in journalItems.OrderByDescending(x => x.Amount))
            {
                var narrative = string.IsNullOrWhiteSpace(journal.Narrative) ? suspenses.FirstOrDefault(x => x.Item.Narrative != string.Empty).Item.Narrative : journal.Narrative;
                var importId = suspenses.FirstOrDefault(x => x.Item.ImportId.HasValue).Item.ImportId.Value;
                var suspenseTransactionDate = suspenses.FirstOrDefault(x => x.Item.ImportId.HasValue).Item.CreatedAt; // we only allow 1 suspense item at the moment but if that changes this is not OK

                // If the journal amount is greater than the remainder of the suspense amount, add a credit note(s) for the difference
                var suspenseAmountRemaining = suspenses.Sum(x => x.AmountRemaining);
                var creditNotesToJournal = new List<TransferItem>();
                if (suspenseAmountRemaining < journal.Amount)
                {
                    var totalCreditNotesToAdd = journal.Amount - suspenseAmountRemaining;
                    creditNotesToJournal = GenerateCreditNoteJournals(creditNotes, importId, totalCreditNotesToAdd, narrative);
                }

                var result = _transactionJournalService.CreateJournal(
                    new TransferItem()
                    {
                        AccountReference = journal.AccountReference,
                        Amount = journal.Amount,
                        FundCode = journal.FundCode,
                        MopCode = journal.MopCode,
                        Narrative = narrative,
                        ImportId = importId,
                        VatCode = journal.VatCode
                    },
                    new TransferItem()
                    {
                        AccountReference = journal.AccountReference,
                        Amount = -(journal.Amount - creditNotesToJournal.Sum(x => x.Amount)),
                        FundCode = _journalFundCode,
                        Narrative = suspenses.FirstOrDefault(x => x.Item.Narrative != string.Empty).Item.Narrative,
                        ImportId = importId,
                        VatCode = _journalVatCode,
                    },
                    creditNotesToJournal,
                    _processId.ToString(),
                    suspenseTransactionDate
                    ).Data as JournalTransaction;

                foreach (var suspense in suspenses.Where(x => x.AmountRemaining > 0))
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
                            TransactionIn = result.Credit,
                            TransactionOut = result.Debit
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
                            TransactionIn = result.Credit,
                            TransactionOut = result.Debit
                        };

                        suspense.Item.SuspenseProcessedTransactions.Add(a);

                        journal.Amount = 0;
                    }

                    if (journal.Amount == 0) break;
                }
            }

            _unitOfWork.Complete(_securityContext.UserId);
        }

        public List<TransferItem> GenerateCreditNoteJournals(List<CreditNote> creditNotes, int importId, decimal totalToCredit, string narrative)
        {
            var creditTransferItems = new List<TransferItem>();

            foreach (var creditNote in creditNotes.Where(x => x.Amount > 0).OrderByDescending(y => y.Amount))
            {
                if (totalToCredit <= 0) continue;

                if (creditNote.Amount >= totalToCredit)
                {
                    creditNote.Amount -= totalToCredit;

                    creditTransferItems.Add(new TransferItem()
                    {
                        AccountReference = creditNote.AccountReference,
                        Amount = totalToCredit,
                        FundCode = creditNote.FundCode,
                        MopCode = _creditMopCode,
                        Narrative = narrative,
                        ImportId = importId,
                        VatCode = creditNote.VatCode
                    });

                    totalToCredit = 0;
                }
                else if (creditNote.Amount < totalToCredit)
                {
                    creditTransferItems.Add(new TransferItem()
                    {
                        AccountReference = creditNote.AccountReference,
                        Amount = creditNote.Amount,
                        FundCode = creditNote.FundCode,
                        MopCode = _creditMopCode,
                        Narrative = narrative,
                        ImportId = importId,
                        VatCode = creditNote.VatCode
                    });

                    totalToCredit -= creditNote.Amount;
                    creditNote.Amount = 0;
                }
            }

            return creditTransferItems;
        }
    }
}
