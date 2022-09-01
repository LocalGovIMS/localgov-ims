using Admin.Models.Transaction;
using BusinessLogic.Interfaces.Services;
using log4net;
using System;

namespace Admin.Classes.ViewModelBuilders.Transaction
{
    public class RefundViewModelBuilder : BaseViewModelBuilder<RefundViewModel, string>
    {
        private readonly ITransactionService _transactionService;

        public RefundViewModelBuilder(ILog log,
            ITransactionService transactionService) : base(log)
        {
            _transactionService = transactionService;
        }

        protected override RefundViewModel OnBuild()
        {
            throw new NotImplementedException();
        }

        protected override RefundViewModel OnBuild(string source)
        {
            var model = new RefundViewModel()
            {
                Reference = source
            };

            var transaction = _transactionService.GetTransactionByPspReference(source);
            var existingRefunds = transaction.ProcessedRefunds;

            if (existingRefunds != null)
            {
                foreach (var transactionLine in transaction.RefundableTransactionLines)
                {
                    var remainingAmount = transaction.AmountAvailableToTransferOrRefundForTransactionLine(transactionLine.TransactionReference);

                    model.RefundItems.Add(new RefundItem()
                    {
                        Transaction = transactionLine,
                        RemainingAmount = remainingAmount
                    });
                }
            }

            return model;
        }
    }
}