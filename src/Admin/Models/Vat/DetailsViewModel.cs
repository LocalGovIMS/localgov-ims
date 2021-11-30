using System.ComponentModel;

namespace Admin.Models.Vat
{
    public class DetailsViewModel
    {
        public string Code { get; set; }

        public decimal? Percentage { get; set; }

        [DisplayName("Status")]
        public bool IsDisabled { get; set; }

        [DisplayName("Status")]
        public string Status => IsDisabled ? "Disabled" : "Active";
    }
}