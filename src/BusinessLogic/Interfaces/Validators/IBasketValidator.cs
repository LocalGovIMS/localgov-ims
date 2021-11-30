using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.Payments;

namespace BusinessLogic.Interfaces.Validators
{
    public interface IBasketValidator
    {
        IResult Validate(Basket basket);
    }
}
