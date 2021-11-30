using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.Vat
{
    public class EditViewModel
    {

        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }

        public decimal? Percentage { get; set; }

        [DisplayName("Disabled")]
        public bool IsDisabled { get; set; }
    }
}