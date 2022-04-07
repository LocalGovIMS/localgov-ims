using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.Fund
{
    public class EditViewModel
    {
        [DisplayName("Fund code")]
        [Required(ErrorMessage = "Fund code is required")]
        public string FundCode { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required")]
        public string FundName { get; set; }

        [DisplayName("Access level")]
        [Required(ErrorMessage = "Access level is required")]
        public string AccessLevel { get; set; }

        [DisplayName("Account reference validator")]
        public int? AccountReferenceValidatorId { get; set; }

        [DisplayName("VAT code")]
        [Required(ErrorMessage = "VAT code is required")]
        public string VatCode { get; set; }

        [DisplayName("Maximum amount")]
        [Required(ErrorMessage = "Maximum amount is required")]
        public decimal? MaximumAmount { get; set; }

        public bool Narrative { get; set; }

        [DisplayName("Export to fund")]
        public bool ExportToFund { get; set; }

        [DisplayName("Export to ledger")]
        public bool ExportToLedger { get; set; }

        [DisplayName("Export to format")]
        [Required(ErrorMessage = "Export to format is required")]
        public string FundExportFormat { get; set; }

        [DisplayName("Use GL code")]
        public bool UseGLCode { get; set; }

        [DisplayName("GL code")]
        [Required(ErrorMessage = "GL code is required")]
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

        [DisplayName("Disabled")]
        public bool IsDisabled { get; set; }

        public SelectList VatCodes { get; set; }
        public SelectList AccountReferenceValidators { get; set; }
    }
}