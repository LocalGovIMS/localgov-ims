using BusinessLogic.Classes;
using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Validators
{
    public class TransactionFeeValidator : ITransactionFeeValidator
    {
        private readonly ITransactionService _transactionService;
        private TransactionFeeValidatorArgs _args;
        private IList<ProcessedTransaction> _transactions;

        public TransactionFeeValidator(
            ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        public IResult Validate(TransactionFeeValidatorArgs args)
        {
            _args = args;

            try
            {
                if (_args == null)
                    throw new NullReferenceException("No args have been provided");

                ValidateFee();

                ValidateMop();

                CheckTheTransactionHasBeenProcessed();

                CheckForExistingFeeTransaction();

                return new Result();
            }
            catch (Exception ex)
            {
                return new Result(ex.Message);
            }
        }

        private void ValidateFee()
        {
            if (_args.PaymentResult.Fee == 0)
                throw new InvalidOperationException("The fee is zero, so does not require processing");
        }

        private void ValidateMop()
        {
            if (!_args.Mop.IncursAFee())
                throw new InvalidOperationException("The method of payment doesn't incur a fee"); // TODO: Should we notify someone this has happened, as fee data does exist, but the MOP is telling us not to store it

        }

        private void CheckTheTransactionHasBeenProcessed()
        {
            _transactions = _transactionService.GetTransactionsByInternalReference(_args.PaymentResult.MerchantReference);

            if (_transactions == null || !_transactions.Any())
                throw new InvalidOperationException("The transaction has not yet been processed");
        }

        private void CheckForExistingFeeTransaction()
        {
            var feeTransaction = _transactions.FirstOrDefault(x => x.Amount == _args.PaymentResult.Fee && x.MopCode == _args.CardPaymentFeeMopCode);

            if (feeTransaction != null)
                throw new InvalidOperationException("A fee transaction already exists");
        }
    }

    public class TransactionFeeValidatorArgs
    {
        public PaymentResult PaymentResult { get; set; }
        public List<PendingTransaction> Transactions { get; set; }
        public string CardPaymentFeeMopCode { get; set; }
        public Mop Mop { get; set; }
    }
}
