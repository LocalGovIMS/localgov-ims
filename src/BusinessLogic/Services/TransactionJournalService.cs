using BusinessLogic.Classes;
using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Strategies;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Services
{
    public class TransactionJournalService : BaseService, ITransactionJournalService
    {
        private readonly string _creditMopCode;
        private readonly string _debitMopCode;

        private ITransactionJournalValidator _transactionJournalValidator;
        private IRollbackTransactionJournalValidator _rollbackTransactionJournalValidator;
        private ITransactionService _transactionService;
        private readonly ITransactionVatStrategy _vatStrategy;

        public TransactionJournalService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext
            , ITransactionJournalValidator transactionJournalValidator
            , IRollbackTransactionJournalValidator rollbackTransactionJournalValidator
            , ITransactionService transactionService
            , ITransactionVatStrategy vatStrategy)
            : base(logger, unitOfWork, securityContext)
        {
            _transactionJournalValidator = transactionJournalValidator;
            _rollbackTransactionJournalValidator = rollbackTransactionJournalValidator;
            _transactionService = transactionService;
            _vatStrategy = vatStrategy;

            _creditMopCode = GetCreditMopCode();
            _debitMopCode = GetDebitMopCode();
        }

        private string GetCreditMopCode()
        {
            return UnitOfWork.Mops.GetAll(true).FirstOrDefault(x => x.IsAJournalReallocation()).MopCode;
        }

        private string GetDebitMopCode()
        {
            return UnitOfWork.Mops.GetAll(true).FirstOrDefault(x => x.IsAJournal()).MopCode;
        }

        public IResult Transfer(List<TransferItem> transferItems, string pspReference, string transactionReference)
        {
            if (!SecurityContext.IsInRole(Security.Role.TransactionJournal)) return new Result("You do not have permission to perform the requested action");

            try
            {
                var transaction = _transactionService.GetTransactionByPspReference(pspReference);
                var validationResult = _transactionJournalValidator.Validate(transaction, transferItems, transactionReference);

                if (validationResult.Success == false) return validationResult;

                foreach (var transferItem in transferItems)
                {
                    CreateTransfer(transferItem, transactionReference);
                }

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("An error occured whilst processing the transfer");
            }
        }

        private void CreateTransfer(TransferItem transferItem, string transctionReference)
        {
            var creditSource = UnitOfWork.Transactions.GetByTransactionReference(transctionReference);

            var entryDate = DateTime.Now;
            var parentTransactionReference = creditSource.TransactionReference;
            var transferGuid = Guid.NewGuid();
            var pspReference = GetNextReferenceId();
            var internalReference = GetNextReferenceId();

            var credit = new ProcessedTransaction
            {
                TransactionReference = GetNextReferenceId(),
                TransactionImportId = creditSource.TransactionImportId,
                InternalReference = internalReference,
                PspReference = pspReference,
                EntryDate = entryDate,
                FundCode = transferItem.FundCode,
                AccountReference = transferItem.AccountReference,
                Amount = transferItem.Amount,
                MopCode = transferItem.MopCode ?? _creditMopCode,
                VatCode = transferItem.VatCode,
                Narrative = string.IsNullOrWhiteSpace(transferItem.Narrative) ? "Journal" : transferItem.Narrative,
                UserCode = SecurityContext.UserId,
                OfficeCode = SecurityContext.OfficeCode,
                TransactionDate = creditSource.TransactionDate,
                TransferReference = transctionReference,
                TransferGuid = transferGuid.ToString()
            };

            _vatStrategy.AddVatToTransaction(credit);

            UnitOfWork.Transactions.Add(credit);

            var debitSource = UnitOfWork.Transactions.GetByTransactionReference(transctionReference);

            var debit = new ProcessedTransaction
            {
                TransactionReference = GetNextReferenceId(),
                TransactionImportId = debitSource.TransactionImportId,
                InternalReference = internalReference,
                PspReference = pspReference,
                EntryDate = entryDate,
                FundCode = debitSource.FundCode,
                Amount = -transferItem.Amount,
                MopCode = _debitMopCode,
                VatCode = debitSource.VatCode,
                AccountReference = debitSource.AccountReference,
                Narrative = string.IsNullOrWhiteSpace(transferItem.Narrative) ? "Journal" : transferItem.Narrative,
                UserCode = SecurityContext.UserId,
                OfficeCode = SecurityContext.OfficeCode,
                TransferReference = transctionReference,
                TransactionDate = creditSource.TransactionDate,
                TransferGuid = transferGuid.ToString()
            };
            _vatStrategy.AddVatToTransaction(debit);

            UnitOfWork.Transactions.Add(debit);
        }

        public IResult CreateJournal(TransferItem transferIn
            , TransferItem transferOut
            , List<TransferItem> creditNotes
            , string transferReference, DateTime suspenseTransactionDate)
        {
            var entryDate = DateTime.Now;
            var transferGuid = Guid.NewGuid();
            var pspReference = GetNextReferenceId();
            var internalReference = GetNextReferenceId();

            ProcessedTransaction credit = null;
            ProcessedTransaction debit = null;

            if (Math.Abs(transferIn.Amount) > 0)
            {
                credit = new ProcessedTransaction
                {
                    TransactionReference = GetNextReferenceId(),
                    TransactionImportId = transferIn.TransactionImportId,
                    InternalReference = internalReference,
                    PspReference = pspReference,
                    EntryDate = entryDate,
                    FundCode = transferIn.FundCode,
                    Amount = transferIn.Amount,
                    MopCode = transferIn.MopCode,
                    VatCode = transferIn.VatCode,
                    AccountReference = transferIn.AccountReference,
                    Narrative = transferIn.Narrative,
                    UserCode = SecurityContext.UserId,
                    OfficeCode = SecurityContext.OfficeCode,
                    TransactionDate = suspenseTransactionDate,
                    TransferReference = transferReference,
                    TransferGuid = transferGuid.ToString()
                };
                _vatStrategy.AddVatToTransaction(credit);

                UnitOfWork.Transactions.Add(credit);
            }

            if (Math.Abs(transferOut.Amount) > 0)
            {
                debit = new ProcessedTransaction
                {
                    TransactionReference = GetNextReferenceId(),
                    TransactionImportId = transferOut.TransactionImportId,
                    InternalReference = internalReference,
                    PspReference = pspReference,
                    EntryDate = entryDate,
                    FundCode = transferOut.FundCode,
                    Amount = transferOut.Amount,
                    MopCode = _debitMopCode,
                    VatCode = transferOut.VatCode,
                    AccountReference = transferOut.AccountReference,
                    Narrative = transferOut.Narrative,
                    UserCode = SecurityContext.UserId,
                    OfficeCode = SecurityContext.OfficeCode,
                    TransferReference = transferReference,
                    TransactionDate = suspenseTransactionDate,
                    TransferGuid = transferGuid.ToString()
                };
                _vatStrategy.AddVatToTransaction(debit);

                UnitOfWork.Transactions.Add(debit);
            }

            var createdCreditNotes = new List<ProcessedTransaction>();
            foreach (var creditNote in creditNotes)
            {
                if (Math.Abs(creditNote.Amount) > 0)
                {
                    var creditNoteTransaction = new ProcessedTransaction
                    {
                        TransactionReference = GetNextReferenceId(),
                        TransactionImportId = creditNote.TransactionImportId,
                        InternalReference = internalReference,
                        PspReference = pspReference,
                        EntryDate = entryDate,
                        FundCode = creditNote.FundCode,
                        Amount = -creditNote.Amount,
                        MopCode = creditNote.MopCode,
                        VatCode = creditNote.VatCode,
                        AccountReference = creditNote.AccountReference,
                        Narrative = creditNote.Narrative,
                        UserCode = SecurityContext.UserId,
                        OfficeCode = SecurityContext.OfficeCode,
                        TransferReference = transferReference,
                        TransactionDate = suspenseTransactionDate,
                        TransferGuid = transferGuid.ToString()
                    };
                    _vatStrategy.AddVatToTransaction(creditNoteTransaction);

                    UnitOfWork.Transactions.Add(creditNoteTransaction);

                    createdCreditNotes.Add(creditNoteTransaction);
                }
            }

            return new Result() { Data = new JournalTransaction(credit, debit, createdCreditNotes) };
        }

        public IResult UndoTransfer(string transferGuid)
        {
            if (!SecurityContext.IsInRole(Security.Role.TransactionJournal)) return new Result("You do not have permission to perform the requested action");

            try
            {
                var transfers = _transactionService.GetTransfers(transferGuid);
                var validationResult = _rollbackTransactionJournalValidator.Validate(transfers);

                if (validationResult.Success == false) return validationResult;

                var rollbackGuid = Guid.NewGuid().ToString();

                foreach (var transfer in transfers)
                {
                    var newTransfer = Utilities.CopyItem(transfer);

                    newTransfer.TransactionReference = GetNextReferenceId();
                    newTransfer.Narrative = "Journal (Undo)";
                    newTransfer.TransferRollbackGuid = rollbackGuid;
                    newTransfer.Amount = -transfer.Amount;
                    _vatStrategy.AddVatToTransaction(newTransfer);

                    // link to the rollback to the transfer
                    transfer.TransferRollbackGuid = rollbackGuid;

                    UnitOfWork.Transactions.Add(newTransfer);
                }

                UnitOfWork.Complete(SecurityContext.UserId);

                return new Result();
            }
            catch (Exception e)
            {
                Logger.Error(null, e);
                return new Result("An error occured whilst processing the transfer");
            }
        }
    }
}
