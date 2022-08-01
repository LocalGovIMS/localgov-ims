using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Models;
using log4net;
using System.Collections.Generic;
using System.Linq;

namespace Admin.Classes.Commands.Validation
{
    public class ValidateTransferItemCommand : BaseCommand<TransferItem>
    {
        private readonly IPaymentValidationHandler _paymentValidationHandler;

        public ValidateTransferItemCommand(ILog log
            , IPaymentValidationHandler paymentValidationHandler)
            : base(log)
        {
            _paymentValidationHandler = paymentValidationHandler;
        }

        protected override CommandResult OnExecute(TransferItem model)
        {
            var errors = new List<string>();

            if (string.IsNullOrEmpty(model.FundCode))
            {
                errors.Add("You must specify a fund.");
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

            var result = _paymentValidationHandler.Validate(new BusinessLogic.Validators.Payment.PaymentValidationArgs()
            {
                Reference = model.AccountReference,
                FundCode = model.FundCode,
                Amount = model.Amount,
            });

            return new CommandResult(result.Success, result.Error);
        }
    }
}