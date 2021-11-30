using BusinessLogic.Interfaces.Result;

namespace BusinessLogic.Interfaces.Validators
{
    public interface IEReturnDescriptionValidator
    {
        IResult Validate(string description, int templateRowId);
    }
}
