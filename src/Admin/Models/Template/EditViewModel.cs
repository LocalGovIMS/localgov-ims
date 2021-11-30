using Admin.Models.Shared;
using BusinessLogic.Entities;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Admin.Models.Template
{
    public class EditViewModel : IValidatableObject
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [DisplayName("Cheque payments")]
        public bool AllowCheque { get; set; }

        [DisplayName("Cash payments")]
        public bool AllowCash { get; set; }

        [DisplayName("PDQ payments")]
        public bool AllowPdq { get; set; }

        public List<TemplateRow> TemplateRows { get; set; }

        public List<BusinessLogic.Entities.Vat> VatList { get; set; }

        public Message Message { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //a.If reference override ticked, reference can have asterix(wild card)
            //b.reference must always be 11 chars long
            //c.Make sure every field is required.

            if (TemplateRows != null)
            {
                // 1. Make sure every field is supplied
                for (var i = 0; i <= TemplateRows.Count - 1; i++)
                {
                    if (string.IsNullOrEmpty(TemplateRows[i].Reference))
                        yield return new ValidationResult("You must provide a reference for every row", new[] { string.Format("TemplateRows[{0}].Reference", i) });

                    if (string.IsNullOrEmpty(TemplateRows[i].Description))
                        yield return new ValidationResult("You must provide a description for every row", new[] { string.Format("TemplateRows[{0}].Description", i) });

                    if (string.IsNullOrEmpty(TemplateRows[i].VatCode))
                        yield return new ValidationResult("You must provide a VAT code for every row", new[] { string.Format("TemplateRows[{0}].Vat_Code", i) });
                }

                // 2. Check reference is 11 characters long
                for (var i = 0; i <= TemplateRows.Count - 1; i++)
                {
                    if (!string.IsNullOrEmpty(TemplateRows[i].Reference) && TemplateRows[i].Reference.Length != 11)
                    {

                        yield return new ValidationResult(string.Format("{0} is not a valid reference - it is not 11 characters long", TemplateRows[i].Reference), new[] { string.Format("TemplateRows[{0}].Reference", i) });
                    }
                }

                // 3. Check reference is made up of correct characters
                for (var i = 0; i <= TemplateRows.Count - 1; i++)
                {
                    if (!string.IsNullOrEmpty(TemplateRows[i].Reference))
                    {
                        if (TemplateRows[i].ReferenceOverride)
                        {
                            // 3a. Check it's digits and asterisks
                            Regex regex = new Regex(@"^[0-9\*]{11,}$");
                            Match match = regex.Match(TemplateRows[i].Reference);
                            if (!match.Success)
                            {
                                yield return new ValidationResult(string.Format("{0} is not a valid reference - it must only contain digits and asterisks", TemplateRows[i].Reference), new[] { string.Format("TemplateRows[{0}].Reference", i) });
                            }
                        }
                        else
                        {
                            // 3b. Check it's digits.
                            Regex regex = new Regex(@"^[0-9]{11,}$");
                            Match match = regex.Match(TemplateRows[i].Reference);
                            if (!match.Success)
                            {
                                yield return new ValidationResult(string.Format("{0} is not a valid reference - it must only contain digits", TemplateRows[i].Reference), new[] { string.Format("TemplateRows[{0}].Reference", i) });
                            }
                        }
                    }
                }
            }
        }
    }
}