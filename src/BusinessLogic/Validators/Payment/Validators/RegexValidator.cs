using System.Text.RegularExpressions;

namespace BusinessLogic.Validators.Payment
{
    public class RegexValidator : AbstractValidator
    {
        protected override void OnValidate(PaymentValidationArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.AccountReferenceValidator.Regex))
                return;

            var regex = new Regex(args.AccountReferenceValidator.Regex);

            if (!regex.IsMatch(args.Reference))
                throw new PaymentValidationException();
        }
    }
}
