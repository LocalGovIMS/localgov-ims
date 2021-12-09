using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;

namespace BusinessLogic.Interfaces.Validators
{
    public interface IAccountHolderStopMessageValidator
    {
        IResult Validate(AccountHolder accountHolder);
    }
}
