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
            ValidateAlphaWhiteSpace(args);
            ValidateNumeric(args);
            ValidateNumericWhiteSpace(args);
            ValidateAlphaNumeric(args);
            ValidateAlphaNumericWhiteSpace(args);
        }

        private void ValidateAlpha(PaymentValidationArgs args)
        {
            if (args.AccountReferenceValidator.CharacterType.Value == CharacterType.Alpha)
            {
                if (!args.Reference.IsAlpha())
                    throw new PaymentValidationException();
            }
        }

        private void ValidateAlphaWhiteSpace(PaymentValidationArgs args)
        {
            if (args.AccountReferenceValidator.CharacterType.Value == CharacterType.AlphaWhiteSpace)
            {
                if (!args.Reference.IsAlphaWhiteSpace())
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

        private void ValidateNumericWhiteSpace(PaymentValidationArgs args)
        {
            if (args.AccountReferenceValidator.CharacterType.Value == CharacterType.NumericWhiteSpace)
            {
                if (!args.Reference.IsNumericWhiteSpace())
                    throw new PaymentValidationException();
            }
        }

        private void ValidateAlphaNumeric(PaymentValidationArgs args)
        {
            if (args.AccountReferenceValidator.CharacterType.Value == CharacterType.AlphaNumeric)
            {
                if (!args.Reference.IsAlphaNumeric())
                    throw new PaymentValidationException();
            }
        }

        private void ValidateAlphaNumericWhiteSpace(PaymentValidationArgs args)
        {
            if (args.AccountReferenceValidator.CharacterType.Value == CharacterType.AlphaNumericWhiteSpace)
            {
                if (!args.Reference.IsAlphaNumericWhiteSpace())
                    throw new PaymentValidationException();
            }
        }
    }
}
