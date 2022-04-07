using BusinessLogic.Interfaces.Validators;

namespace BusinessLogic.Validators.Payment
{
    public abstract class AbstractValidator : IValidator<PaymentValidationArgs>
    {
        private IValidator<PaymentValidationArgs> _nextValidator;

        public IValidator<PaymentValidationArgs> SetNext(IValidator<PaymentValidationArgs> validator)
        {
            this._nextValidator = validator;

            return validator;
        }

        public virtual void Validate(PaymentValidationArgs request)
        {
            OnValidate(request);

            if (this._nextValidator == null)
                return;

            this._nextValidator.Validate(request); 
        }

        protected abstract void OnValidate(PaymentValidationArgs request);
    }
}
