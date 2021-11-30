using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using System.Text.RegularExpressions;

namespace BusinessLogic.Validators
{
    public class EReturnReferenceValidator : IEReturnReferenceValidator
    {
        private ITemplateRowService _templateRowService;

        public EReturnReferenceValidator(ITemplateRowService templateRowService)
        {
            _templateRowService = templateRowService;
        }

        public IResult Validate(string reference, int templateRowId)
        {
            if (string.IsNullOrEmpty(reference)) return new Result("Reference is empty");

            var templateRow = _templateRowService.GetTemplateRow(templateRowId);

            if (templateRow.ReferenceOverride == false
                && !reference.Equals(templateRow.Reference))
                return new Result(string.Format("Reference '{0}' cannot be overridden, but is set to '{1}'", templateRow.Reference, reference));

            for (var i = 0; i <= templateRow.Reference.Length - 1; i++)
            {
                var templateValue = templateRow.Reference[i];
                var referenceValue = reference[i];

                if (templateValue.Equals('*'))
                {
                    // Check to see if reference value is a valid character
                    Regex regex = new Regex(@"^[0-9]$");
                    Match match = regex.Match(referenceValue.ToString());
                    if (!match.Success)
                    {
                        return new Result(string.Format("{0} is not a valid reference - it must only contain digits",
                            reference));
                    }
                }
                else
                {
                    if (!templateValue.Equals(referenceValue))
                        return new Result(string.Format("Reference '{0}' is invalid for mask '{1}' at position {2}",
                            reference,
                            templateRow.Reference,
                            i + 1));
                }
            }

            // If we got this far, all is OK....
            return new Result();
        }
    }
}
