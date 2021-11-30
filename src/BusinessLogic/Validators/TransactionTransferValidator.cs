using BusinessLogic.Classes.Result;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Models;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Validators
{
    public class TransactionTransferValidator : ITransactionTransferValidator
    {
        private IAccountReferenceValidator _accountReferenceValidator;

        public TransactionTransferValidator(IAccountReferenceValidator accountReferenceValidator)
        {
            _accountReferenceValidator = accountReferenceValidator;
        }

        public IResult Validate(TransferItem sourceItem, IList<TransferItem> transferItems)
        {
            if (sourceItem == null) return new Result("Unable to find a source transaction to validate transfers against");
            if (transferItems == null || !transferItems.Any()) return new Result("Unable to find any valid transfers");

            // Validate source item
            var validateAccountReferenceResult = ValidateAccountReference(sourceItem);
            if (!validateAccountReferenceResult.Success) return validateAccountReferenceResult;

            var validateAmountResult = ValidateAmount(sourceItem);
            if (!validateAmountResult.Success) return validateAmountResult;

            // Validate individual items
            foreach (var item in transferItems)
            {
                validateAccountReferenceResult = ValidateAccountReference(item);
                if (!validateAccountReferenceResult.Success) return validateAccountReferenceResult;

                validateAmountResult = ValidateAmount(item);
                if (!validateAmountResult.Success) return validateAmountResult;
            }

            var validateTotalAmountResult = ValidateTotalAmount(sourceItem, transferItems);
            if (!validateTotalAmountResult.Success) return validateTotalAmountResult;

            // If we got this far, all is OK....
            return new Result();
        }

        public IResult Validate(IList<TransferItem> sourceItems, IList<TransferItem> transferItems)
        {
            if (sourceItems == null || !sourceItems.Any()) return new Result("Unable to find a source transaction to validate transfers against");
            if (transferItems == null || !transferItems.Any()) return new Result("Unable to find any valid transfers");

            if (sourceItems.Count > 0 && sourceItems.Count != transferItems.Count) return new Result("Number of target transfers must match number of source transactions");

            for (var i = 0; i < sourceItems.Count; i++)
            {
                if (sourceItems[i].Amount != transferItems[i].Amount) return new Result("Each source transaction amount must match the target transfer amount");

                var validateAccountReferenceResult = ValidateAccountReference(sourceItems[i]);
                if (!validateAccountReferenceResult.Success) return validateAccountReferenceResult;

                var validateAmountResult = ValidateAmount(sourceItems[i]);
                if (!validateAmountResult.Success) return validateAmountResult;

                validateAccountReferenceResult = ValidateAccountReference(transferItems[i]);
                if (!validateAccountReferenceResult.Success) return validateAccountReferenceResult;

                validateAmountResult = ValidateAmount(transferItems[i]);
                if (!validateAmountResult.Success) return validateAmountResult;
            }

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

        private IResult ValidateTotalAmount(TransferItem sourceItem
            , IList<TransferItem> transferItems)
        {
            var validationResult = new Result();

            var totalToTransfer = transferItems.Sum(x => x.Amount);

            // Check total amount etc.
            if (totalToTransfer > sourceItem.Amount)
                validationResult.AddError(string.Format("The total amount to journal (£{0}) is greater than the amount available to journal (£{1})", decimal.Round(totalToTransfer, 2).ToString("N"), sourceItem.Amount.ToString("N")));

            return validationResult;
        }
    }
}
