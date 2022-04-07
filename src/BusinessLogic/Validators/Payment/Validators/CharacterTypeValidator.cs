using BusinessLogic.Enums;
using BusinessLogic.Extensions;

namespace BusinessLogic.Validators.Payment
{
    public class CharacterTypeValidator : AbstractValidator
    {
        protected override void OnValidate(PaymentValidationArgs args)
        {
            if (args.AccountReferenceValidator.CharacterType.HasValue)
                ValidateCharacters(args);
        }

        private void ValidateCharacters(PaymentValidationArgs args)
        {
            ValidateAlpha(args);
            ValidateNumeric(args);
            ValidateAlphaNumeric(args);
        }

        private void ValidateAlpha(PaymentValidationArgs args)
        {
            if (args.AccountReferenceValidator.CharacterType.Value == CharacterType.Alpha)
            {
                if (!args.Reference.IsAlpha())
                    throw new PaymentValidationException();
            }
        }

        private void ValidateNumeric(PaymentValidationArgs args)
        {
            if (args.AccountReferenceValidator.CharacterType.Value == CharacterType.Numeric)
            {
                if (!args.Reference.IsNumeric())
                    throw new PaymentValidationException();
            }
        }

        private void ValidateAlphaNumeric(PaymentValidationArgs args)
        {
            var alphaNumeric = CharacterType.Alpha & CharacterType.Numeric;
            if (args.AccountReferenceValidator.CharacterType.Value.HasFlag(alphaNumeric))
            {
                if (!args.Reference.IsAlphaNumeric())
                    throw new PaymentValidationException();
            }
        }
    }
}
