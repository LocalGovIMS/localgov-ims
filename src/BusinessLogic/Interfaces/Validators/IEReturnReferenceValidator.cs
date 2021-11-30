using BusinessLogic.Interfaces.Result;

namespace BusinessLogic.Interfaces.Validators
{
    public interface IEReturnReferenceValidator
    {
        IResult Validate(string reference, int templateRowId);
    }
}
