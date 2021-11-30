using BusinessLogic.Classes.Result;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Models;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Validators
{
    public class TransactionJournalValidator : ITransactionJournalValidator
    {
        private IAccountReferenceValidator _accountReferenceValidator;

        public TransactionJournalValidator(IAccountReferenceValidator accountReferenceValidator)
        {
            _accountReferenceValidator = accountReferenceValidator;
        }

        public IResult Validate(
            Transaction transaction
            , IList<TransferItem> transferItems
            , string transactionReference)
        {
            if (transaction == null) return new Result("Unable to find the transaction to validate transfers against");

            // Validate individual items
            foreach (var item in transferItems)
            {
                var validateAccountReferenceResult = ValidateAccountReference(item);
                if (!validateAccountReferenceResult.Success) return validateAccountReferenceResult;

                var validateAmountResult = ValidateAmount(item);
                if (!validateAmountResult.Success) return validateAmountResult;
            }

            var validateTotalAmountResult = ValidateTotalAmount(transaction, transferItems, transactionReference);
            if (!validateTotalAmountResult.Success) return validateTotalAmountResult;

            // If we got this far, all is OK....
            return new Result();
        }

        private IResult ValidateAccountReference(TransferItem item)
        {
            return _accountReferenceValidator.ValidateReference(item.AccountReference, item.FundCode, item.Amount, AccountReferenceValidationSource.Payments);
        }

        private IResult ValidateAmount(TransferItem item)
        {
            var validationResult = new Result();

            if (item.Amount < 0)
            {
                validationResult.AddError("The amount cannot be less than zero");
            }

            if (item.Amount == 0)
            {
                validationResult.AddError("The amount cannot be zero");
            }

            return validationResult;
        }

        private IResult ValidateTotalAmount(Transaction transaction
            , IList<TransferItem> transferItems
            , string transactionReference)
        {
            var validationResult = new Result();

            var totalToTransfer = transferItems.Sum(x => x.Amount);

            // Check total amount etc.
            if (totalToTransfer > transaction.AmountAvailableToTransferOrRefundForTransactionLine(transactionReference))
                validationResult.AddError(string.Format("The total amount to journal (£{0}) is greater than the amount available to journal (£{1})", decimal.Round(totalToTransfer, 2).ToString("N"), transaction.AmountAvailableToTransferOrRefundForTransactionLine(transactionReference).ToString("N")));

            return validationResult;
        }
    }
}
