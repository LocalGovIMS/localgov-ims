using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Result;

namespace BusinessLogic.Interfaces.Services
{
    public interface IValidationService
    {
        IResult ValidateReference(string reference, string fundCode, decimal amount, AccountReferenceValidationSource source);
    }
}
