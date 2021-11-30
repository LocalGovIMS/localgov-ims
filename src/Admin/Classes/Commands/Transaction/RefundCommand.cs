using Admin.Models.Shared;
using Admin.Models.Transaction;
using BusinessLogic.Classes;
using BusinessLogic.Interfaces.Services;
using log4net;
using System.Collections.Generic;

namespace Admin.Classes.Commands.Transaction
{
    public class RefundCommand : BaseCommand<RefundViewModel>
    {
        private readonly IRefundService _refundService;

        public RefundCommand(ILog log
            , IRefundService refundService)
            : base(log)
        {
            _refundService = refundService;
        }

        protected override CommandResult OnExecute(RefundViewModel model)
        {
            var refundRequests = new List<RefundRequest>();
            foreach (var refundItem in model.RefundItems)
            {
                refundRequests.Add(new RefundRequest()
                {
                    RefundAmount = refundItem.RefundAmount,
                    TransactionReference = refundItem.Transaction.TransactionReference
                });
            }

            var refundStatus = _refundService.RefundTransactions(refundRequests, model.RefundReason);

            var result = new CommandResult(true);

            switch (refundStatus.Status)
            {
                case RefundStatusType.Accepted:
                case RefundStatusType.Success:
                    result.Data = new SuccessMessage(
                        "Your refund is being processed, this may take several minutes, you can see the status below",
                        "We've requested your refund"
                        );
                    break;
                case RefundStatusType.Error:
                case RefundStatusType.Failed:
                    result.Data = new ErrorMessage(
                        refundStatus.Message,
                        "We couldn't process your refund"
                        );
                    break;
            }

            return result;
        }
    }
}