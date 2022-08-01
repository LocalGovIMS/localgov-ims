using System.ComponentModel;

namespace Admin.Models.Fund
{
    public class DetailsViewModel
    {
        [DisplayName("Fund")]
        public string FundCode { get; set; }

        [DisplayName("Name")]
        public string FundName { get; set; }

        [DisplayName("Account reference validator")]
        public int? AccountReferenceValidatorId { get; set; }

        [DisplayName("Account reference validator")]
        public string AccountReferenceValidatorName { get; set; }

        [DisplayName("VAT code")]
        public string VatCode { get; set; }

        [DisplayName("Maximum amount")]
        public decimal? MaximumAmount { get; set; }

        [DisplayName("Overpay account")]
        public bool OverPayAccount { get; set; }

        [DisplayName("Account exist")]
        public bool AccountExist { get; set; }

        [DisplayName("Aquire address")]
        public bool AquireAddress { get; set; }

        [DisplayName("Display name")]
        public string DisplayName { get; set; }

        [DisplayName("Allow VAT override")]
        public bool VatOverride { get; set; }

        [DisplayName("Status")]
        public bool IsDisabled { get; set; }

        [DisplayName("Status")]
        public string Status => IsDisabled ? "Disabled" : "Active";
    }
}