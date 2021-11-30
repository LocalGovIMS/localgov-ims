using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Validators;
using System.Collections.Generic;

namespace BusinessLogic.Validators
{
    public class RollbackTransactionJournalValidator : IRollbackTransactionJournalValidator
    {
        public IResult Validate(IList<ProcessedTransaction> transfers)
        {
            if (transfers == null) return new Result("Unable to find the transfers to rollback");

            var validationResult = new Result();

            // Validate individual items
            foreach (var item in transfers)
            {
                ValidateTransferRollbackGuid(item.TransferRollbackGuid, validationResult);
                if (!validationResult.Success) break;
            }

            return validationResult;
        }

        private void ValidateTransferRollbackGuid(string transferRollbackGuid, Result validationResult)
        {
            if (!string.IsNullOrEmpty(transferRollbackGuid))
            {
                validationResult.AddError("Transfer has already been rolled back");
            }
        }
    }
}
