namespace BusinessLogic.Validators.Payment
{
    public class AmountValidator : AbstractValidator
    {
        protected override void OnValidate(PaymentValidationArgs args)
        {
            if (!args.Fund.MaximumAmount.HasValue)
                return;

            if (args.Amount > args.Fund.MaximumAmount)
                throw new PaymentValidationException("The maximum amount you can pay for this type is £" + decimal.Round((decimal)args.Fund.MaximumAmount, 2));
        }
    }
}
