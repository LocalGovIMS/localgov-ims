using BusinessLogic.Interfaces.Result;
using BusinessLogic.Validators.Payment;

namespace BusinessLogic.Interfaces.Validators
{
    public interface IPaymentValidationHandler
    {
        IResult Validate(PaymentValidationArgs args);
    }
}