using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;

namespace BusinessLogic.Interfaces.Validators
{
    public interface ITemplateRowValidator
    {
        IResult Validate(TemplateRow templateRow);
    }
}
