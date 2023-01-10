using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using System.Text.RegularExpressions;

namespace BusinessLogic.Validators
{
    public class EReturnReferenceValidator : IEReturnReferenceValidator
    {
        private IEReturnTemplateRowService _eReturnTemplateRowService;

        public EReturnReferenceValidator(IEReturnTemplateRowService eReturnTemplateRowService)
        {
            _eReturnTemplateRowService = eReturnTemplateRowService;
        }

        public IResult Validate(string reference, int eReturnTemplateRowId)
        {
            if (string.IsNullOrEmpty(reference)) return new Result("Reference is empty");

            var eReturnTemplateRow = _eReturnTemplateRowService.Get(eReturnTemplateRowId);

            if (eReturnTemplateRow.ReferenceOverride == false
                && !reference.Equals(eReturnTemplateRow.Reference))
                return new Result(string.Format("Reference '{0}' cannot be overridden, but is set to '{1}'", eReturnTemplateRow.Reference, reference));

            for (var i = 0; i <= eReturnTemplateRow.Reference.Length - 1; i++)
            {
                var templateValue = eReturnTemplateRow.Reference[i];
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
                            eReturnTemplateRow.Reference,
                            i + 1));
                }
            }

            // If we got this far, all is OK....
            return new Result();
        }
    }
}
