using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Strategies;
using log4net;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Classes.Strategies
{
    public class ApproveEReturnsStrategy : IApproveEReturnsStrategy
    {
        private readonly ILog _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISecurityContext _securityContext;
        private readonly ITransactionVatStrategy _vatStrategy;
        private readonly Guid _processId;

        public ApproveEReturnsStrategy(
            ILog logger
            , IUnitOfWork unitOfWork
            , ISecurityContext securityContext
            , ITransactionVatStrategy vatStrategy)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _securityContext = securityContext;
            _vatStrategy = vatStrategy;
            _processId = Guid.NewGuid();
        }

        public IResult Execute(List<Tuple<int, string>> eReturns)
        {
            try
            {
                Lock(eReturns.Select(x => x.Item1).ToList());

                var data = GetLockedItems();

                var validationResult = Validate(data, eReturns);
                if (!validationResult.Success) return validationResult;

                return Approve(data, eReturns);
            }
            catch (Exception e)
            {
                _logger.Error(e);
                return new Result.Result("Unable to approve the selected eReturns");
            }
            finally
            {
                Unlock();
            }
        }

        private void Lock(List<int> items)
        {
            _unitOfWork.EReturns.Lock(items, _processId);
            _unitOfWork.Complete(_securityContext.UserId);
        }

        private void Unlock()
        {
            _unitOfWork.EReturns.Unlock(_processId);
            _unitOfWork.Complete(_securityContext.UserId);
        }

        private List<EReturn> GetLockedItems()
        {
            return _unitOfWork.EReturns.GetEReturnsBeingProcessed(_processId).ToList();
        }

        private IResult Validate(List<EReturn> items, List<Tuple<int, string>> sourceItems)
        {
            /* Rules:
             * 
             * 1. We must have some eReturns!
             * 2. All must be submitted (and therefore awaiting approval)
             * 3. None should have any TransactionProcessed records
             * ??
             */

            if (!items.Any())
            {
                return new Result.Result("You must choose some eReturns");
            }

            if (items.Any(x => x.EReturnStatus.Id != (int)Enums.EReturnStatus.Submitted))
            {
                _logger.WarnFormat("Trying to approve eReturns that are not in a submitted state: {0}"
                    , string.Join(",", items.Where(x => x.EReturnStatus.Id != (int)Enums.EReturnStatus.Submitted).Select(x => x.EReturnNo).ToList()));

                return new Result.Result("Some of the eReturns selected are not in the correct state");
            }

            if (items.Count != sourceItems.Count)
                return new Result.Result("Unable to process all of the records requested.");

            return new Result.Result();
        }

        private IResult Approve(List<EReturn> items, List<Tuple<int, string>> eReturns)
        {
            /*

            1.UserId is who approved it.
            2.Add lock mechanism - like suspense, so if an eReturn is being approved no one else can process it at the same time
            3.Check data can still be approved(validation checks - is it in the correct state(submitted), etc.)
            4.Update EReturn record -add approved by user, and datetime
            5.Create ProcessedTransaction records
            6.Update status of EReturn record -set to 'Approved'
            7.Unlock

            */

            foreach (var item in items)
            {
                item.ApprovedByUserId = _securityContext.UserId;
                item.ApprovedAt = DateTime.Now;

                if (item.StatusId != (int)Enums.EReturnStatus.Submitted)
                    return new Result.Result(string.Format("EReturn '{0}' is not in the correct state", item.EReturnNo));

                var transactions = new List<ProcessedTransaction>();

                foreach (var pendingTransaction in item.PendingTransactions)
                {
                    pendingTransaction.StatusId = (int)Enums.TransactionStatus.Successful;
                    pendingTransaction.Processed = true;

                    // Only copy it over if there is a value.
                    if (!pendingTransaction.Amount.HasValue
                        || pendingTransaction.Amount == 0) continue;

                    var transaction = Mapper.Map<ProcessedTransaction>(pendingTransaction);

                    transaction.TransactionDate = item.SubmittedAt;
                    transaction.EntryDate = DateTime.Now;

                    transaction.UserCode = _securityContext.UserId;
                    transaction.User = null; // Needed to make sure the User_Code field gets set, rather than the link to the existing user being persisted.

                    transaction.OfficeCode = _securityContext.OfficeCode;
                    transaction.Office = null;

                    // Look at swapping the dates around tomorrow...

                    transaction.PspReference = eReturns.Where(x => x.Item1 == item.Id).FirstOrDefault().Item2;
                    _vatStrategy.AddVatToTransaction(transaction);

                    transactions.Add(transaction);
                }

                _unitOfWork.Transactions.AddRange(transactions);
                item.StatusId = (int)Enums.EReturnStatus.Authorised;
            }

            // This will add the TransactionProcessed records and udpate the TransactionPending records.
            _unitOfWork.Complete(_securityContext.UserId);

            return new Result.Result();
        }
    }
}
