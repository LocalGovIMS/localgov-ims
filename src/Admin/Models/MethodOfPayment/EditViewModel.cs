using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.MethodOfPayment
{
    public class EditViewModel : IValidatableObject
    {
        [Required]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage = "Maximum amount is required")]
        [DisplayName("Maximum amount")]
        public decimal? MaximumAmount { get; set; }

        [Required(ErrorMessage = "Minimum amount is required")]
        [DisplayName("Minimum amount")]
        public decimal? MinimumAmount { get; set; }

        [DisplayName("Disabled")]
        public bool IsDisabled { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (MaximumAmount < MinimumAmount)
                yield return new ValidationResult("The maximum amount must be greater than the minimum amount");
        }
    }
}