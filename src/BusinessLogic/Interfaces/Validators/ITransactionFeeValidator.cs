using BusinessLogic.Interfaces.Result;
using BusinessLogic.Validators;

namespace BusinessLogic.Interfaces.Validators
{
    public interface ITransactionFeeValidator
    {
        IResult Validate(TransactionFeeValidatorArgs args);
    }
}