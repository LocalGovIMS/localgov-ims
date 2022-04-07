namespace BusinessLogic.Validators.Payment
{
    public class ReferenceLengthValidator : AbstractValidator
    {
        protected override void OnValidate(PaymentValidationArgs args)
        {
            if (string.IsNullOrWhiteSpace(args.Reference))
                throw new PaymentValidationException();

            if (args.Reference.Length > args.AccountReferenceValidator.MaxLength
                || args.Reference.Length < args.AccountReferenceValidator.MinLength)
            {
                throw new PaymentValidationException();
            }
        }
    }
}
