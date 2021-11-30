using System.ComponentModel;

namespace Admin.Models.Fund
{
    public class DetailsViewModel
    {
        [DisplayName("Fund code")]
        public string FundCode { get; set; }

        [DisplayName("Name")]
        public string FundName { get; set; }

        [DisplayName("Access level")]
        public string AccessLevel { get; set; }

        [DisplayName("Validation reference")]
        public string ValidationReference { get; set; }

        [DisplayName("VAT code")]
        public string VatCode { get; set; }

        [DisplayName("Maximum amount")]
        public decimal? MaximumAmount { get; set; }

        public bool Narrative { get; set; }

        [DisplayName("Export to fund")]
        public bool ExportToFund { get; set; }

        [DisplayName("Export to ledger")]
        public bool ExportToLedger { get; set; }

        [DisplayName("Export to format")]
        public string FundExportFormat { get; set; }

        [DisplayName("Use GL code")]
        public bool UseGLCode { get; set; }

        [DisplayName("GL code")]
        public string GLCode { get; set; }

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