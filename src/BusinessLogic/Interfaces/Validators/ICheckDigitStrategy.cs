using BusinessLogic.Validators.Payment;

namespace BusinessLogic.Interfaces.Validators
{
    public interface ICheckDigitStrategy
    {
        void Validate(CheckDigitStrategyArgs args);
    }
}
