namespace BusinessLogic.Validators.Payment
{
    public class AmountValidator : AbstractValidator
    {
        protected override void OnValidate(PaymentValidationArgs args)
        {
            ValidateAmountForFund(args);
            ValidateAmountForMop(args);
        }

        private void ValidateAmountForFund(PaymentValidationArgs args)
        {
            if (!args.Fund.MaximumAmount.HasValue)
                return;

            if (args.Amount > args.Fund.MaximumAmount)
                throw new PaymentValidationException("The maximum amount you can pay for this type is £" + decimal.Round((decimal)args.Fund.MaximumAmount, 2));
        }

        private void ValidateAmountForMop(PaymentValidationArgs args)
        {
            if (args.Mop is null)
                return;

            if (args.Amount > args.Mop.MaximumAmount)
                throw new PaymentValidationException("The maximum amount you can pay via this payment methhod is £" + decimal.Round((decimal)args.Mop.MaximumAmount, 2));

            if (args.Amount < args.Mop.MinimumAmount)
                throw new PaymentValidationException("The minimum amount you can pay via this payment methhod is £" + decimal.Round((decimal)args.Mop.MinimumAmount, 2));
        }
    }

}
