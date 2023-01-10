using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;

namespace BusinessLogic.Validators
{
    public class EReturnDescriptionValidator : IEReturnDescriptionValidator
    {
        private IEReturnTemplateRowService _eReturnTemplateRowService;

        public EReturnDescriptionValidator(IEReturnTemplateRowService eReturnTemplateRowService)
        {
            _eReturnTemplateRowService = eReturnTemplateRowService;
        }

        public IResult Validate(string description, int eReturnTemplateRowId)
        {
            if (string.IsNullOrEmpty(description)) return new Result("Description is empty");

            var eReturnTemplateRow = _eReturnTemplateRowService.Get(eReturnTemplateRowId);

            if (eReturnTemplateRow.DescriptionOverride == false
                && !description.Equals(eReturnTemplateRow.Description))
                return new Result(string.Format("Description '{0}' cannot be overridden, but is set to '{1}'", eReturnTemplateRow.Reference, description));

            // If we got this far, all is OK....
            return new Result();
        }
    }
}
