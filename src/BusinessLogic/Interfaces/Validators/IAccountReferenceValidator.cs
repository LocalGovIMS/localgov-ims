using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Result;

namespace BusinessLogic.Interfaces.Validators
{
    public interface IAccountReferenceValidator
    {
        IResult ValidateReference(string reference, string fundCode, decimal amount, AccountReferenceValidationSource source);
    }
}
