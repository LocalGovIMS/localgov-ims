using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.Fund
{
    public class EditViewModel
    {
        [DisplayName("Fund code")]
        [Required(ErrorMessage = "Fund code is required")]
        [StringLength(2, ErrorMessage = "Fund code cannot be longer than 2 characters")]
        public string FundCode { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Fund name cannot be longer than 50 characters")]
        public string FundName { get; set; }

        [DisplayName("Access level")]
        [Required(ErrorMessage = "Access level is required")]
        [StringLength(2, ErrorMessage = "Access level cannot be longer than 2 characters")]
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
        [StringLength(10, ErrorMessage = "Export to format cannot be longer than 10 characters")]
        public string FundExportFormat { get; set; }

        [DisplayName("Use GL code")]
        public bool UseGLCode { get; set; }

        [DisplayName("GL code")]
        [Required(ErrorMessage = "GL code is required")]
        [StringLength(20, ErrorMessage = "GL code cannot be longer than 20 characters")]
        public string GLCode { get; set; }

        [DisplayName("Overpay account")]
        public bool OverPayAccount { get; set; }

        [DisplayName("Account exist")]
        public bool AccountExist { get; set; }

        [DisplayName("Aquire address")]
        public bool AquireAddress { get; set; }

        [DisplayName("Display name")]
        [StringLength(50, ErrorMessage = "Display name cannot be longer than 50 characters")]
        public string DisplayName { get; set; }

        [DisplayName("Allow VAT override")]
        public bool VatOverride { get; set; }

        [DisplayName("Disabled")]
        public bool IsDisabled { get; set; }

        public SelectList VatCodes { get; set; }
        public SelectList AccountReferenceValidators { get; set; }
    }
}