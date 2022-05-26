using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;

namespace BusinessLogic.Interfaces.Validators
{
    public interface IAccountHolderFundMessageValidator
    {
        IResult Validate(AccountHolder accountHolder);
    }
}
