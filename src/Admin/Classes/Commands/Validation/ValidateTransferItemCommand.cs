using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Models;
using log4net;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Classes.Commands.Validation
{
    public class ValidateTransferItemCommand : BaseCommand<TransferItem>
    {
        private readonly IAccountReferenceValidator _accountReferenceValidator;

        public ValidateTransferItemCommand(ILog log
            , IAccountReferenceValidator accountReferenceValidator)
            : base(log)
        {
            _accountReferenceValidator = accountReferenceValidator;
        }

        protected override CommandResult OnExecute(TransferItem model)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(model.FundCode))
            {
                errors.Add("You must specify a fund code.");
            }

            if (string.IsNullOrEmpty(model.AccountReference))
            {
                errors.Add("You must specify an account reference.");
            }

            if (model.FundCode == "01")
            {
                errors.Add("You can not journal to suspense");
            }

            if (errors.Any())
            {
                return new CommandResult(false, errors);
            }

            var result = _accountReferenceValidator.ValidateReference(
                model.AccountReference,
                model.FundCode,
                model.Amount,
                AccountReferenceValidationSource.Payments);

            return new CommandResult(result.Success, result.Error);
        }
    }
}