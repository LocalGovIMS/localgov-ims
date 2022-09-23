using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Strategies;
using BusinessLogic.Models;
using log4net;
using System;

namespace BusinessLogic.Services
{
    public class SuspenseJournalService : BaseService, ISuspenseJournalService
    {
        private readonly ITransactionVatStrategy _vatStrategy;

        public SuspenseJournalService(ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext
            , ITransactionVatStrategy vatStrategy)
            : base(logger, unitOfWork, securityContext)
        {
            _vatStrategy = vatStrategy;
        }

        public string GetPspReference()
        {
            return GetNextReferenceId();
        }

        public IResult CreateJournal(
            TransferItem journal,
            Guid transferReference,
            Guid transferGuid,
            string pspReference,
            DateTime suspenseTransactionDate)
        {
            ProcessedTransaction credit = null;

            if (Math.Abs(journal.Amount) > 0)
            {
                credit = new ProcessedTransaction
                {
                    TransactionReference = GetNextReferenceId(),
                    ImportId = journal.ImportId,
                    InternalReference = GetNextReferenceId(),
                    PspReference = pspReference,
                    EntryDate = DateTime.Now,
                    FundCode = journal.FundCode,
                    Amount = journal.Amount,
                    MopCode = journal.MopCode,
                    VatCode = journal.VatCode,
                    AccountReference = journal.AccountReference,
                    Narrative = journal.Narrative,
                    UserCode = SecurityContext.UserId,
                    OfficeCode = SecurityContext.OfficeCode,
                    TransactionDate = suspenseTransactionDate,
                    TransferReference = transferReference.ToString(),
                    TransferGuid = transferGuid.ToString()
                };
                _vatStrategy.AddVatToTransaction(credit);

                UnitOfWork.Transactions.Add(credit);
            }

            return new Result() { Data = credit };
        }
    }
}
