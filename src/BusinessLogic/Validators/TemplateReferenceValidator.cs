using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Interfaces.Validators;
using log4net;
using System.Text.RegularExpressions;

namespace BusinessLogic.Validators
{
    public class TemplateRowValidator : ITemplateRowValidator
    {
        private readonly ILog _logger;
        private const string OverridableTemplateReferencePattern = @"^[0-9\*]{11,}$";
        private const string NonOverridableTemplateReferencePattern = @"^[0-9]{11,}$";

        public TemplateRowValidator(ILog logger)
        {
            _logger = logger;
        }

        public IResult Validate(TemplateRow templateRow)
        {
            if (templateRow == null) return new Result("There is no template row to validate");

            _logger.DebugFormat("Validating template row: {0}", templateRow.Reference);


            if (string.IsNullOrEmpty(templateRow.Reference)) return new Result("The reference is missing");
            if (string.IsNullOrEmpty(templateRow.Description)) return new Result("The description is missing");
            if (string.IsNullOrEmpty(templateRow.VatCode)) return new Result("The VAT code is missing");

            if (templateRow.Reference.Length != 11) return new Result("The reference should be 11 characters long");

            if (templateRow.ReferenceOverride)
            {
                // 3a. Check it's digits and asterisks
                Regex regex = new Regex(@"^[0-9\*]{11,}$");
                Match match = regex.Match(templateRow.Reference);
                if (!match.Success)
                {
                    return new Result(string.Format("{0} is not a valid reference - it must only contain digits and asterisks", templateRow.Reference));
                }
            }
            else
            {
                // 3b. Check it's digits.
                Regex regex = new Regex(@"^[0-9]{11,}$");
                Match match = regex.Match(templateRow.Reference);
                if (!match.Success)
                {
                    return new Result(string.Format("{0} is not a valid reference - it must only contain digits", templateRow.Reference));
                }
            }

            return new Result();
        }
    }
}