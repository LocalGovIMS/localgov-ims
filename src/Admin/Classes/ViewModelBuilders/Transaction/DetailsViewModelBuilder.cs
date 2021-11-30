using Admin.Models.Transaction;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;
using System.Linq;

namespace Admin.Classes.ViewModelBuilders.Transaction
{
    public class DetailsViewModelBuilder : BaseViewModelBuilder<DetailsViewModel, string>
    {
        private readonly ITransactionService _transactionService;

        public DetailsViewModelBuilder(ILog log,
            ITransactionService transactionService)
            : base(log)
        {
            _transactionService = transactionService;
        }

        protected override DetailsViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override DetailsViewModel OnBuild(string pspReference)
        {
            var transaction = _transactionService.GetTransactionByPspReference(pspReference);
            var refund = new RefundViewModel
            {
                Reference = pspReference
            };

            if (transaction.ProcessedRefunds != null)
            {
                refund.RefundItems = transaction.TransactionLines
                    .Select(x => new RefundItem()
                    {
                        Transaction = x,
                        RemainingAmount = transaction.AmountAvailableToTransferOrRefundForTransactionLine(x.TransactionReference)
                    }).ToList();
            }

            var model = new DetailsViewModel()
            {
                Transaction = transaction,
                Refund = refund
            };

            return model;
        }
    }
}