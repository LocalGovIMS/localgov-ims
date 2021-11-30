using System.ComponentModel;

namespace Admin.Models.MethodOfPayment
{
    public class DetailsViewModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        [DisplayName("Maximum amount")]
        public decimal? MaximumAmount { get; set; }

        [DisplayName("Minimum amount")]
        public decimal? MinimumAmount { get; set; }

        [DisplayName("Status")]
        public bool IsDisabled { get; set; }

        [DisplayName("Status")]
        public string Status => IsDisabled ? "Disabled" : "Active";
    }
}