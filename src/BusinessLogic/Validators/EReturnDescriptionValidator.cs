using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;

namespace BusinessLogic.Validators
{
    public class EReturnDescriptionValidator : IEReturnDescriptionValidator
    {
        private ITemplateRowService _templateRowService;

        public EReturnDescriptionValidator(ITemplateRowService templateRowService)
        {
            _templateRowService = templateRowService;
        }

        public IResult Validate(string description, int templateRowId)
        {
            if (string.IsNullOrEmpty(description)) return new Result("Description is empty");

            var templateRow = _templateRowService.GetTemplateRow(templateRowId);

            if (templateRow.DescriptionOverride == false
                && !description.Equals(templateRow.Description))
                return new Result(string.Format("Description '{0}' cannot be overridden, but is set to '{1}'", templateRow.Reference, description));

            // If we got this far, all is OK....
            return new Result();
        }
    }
}
