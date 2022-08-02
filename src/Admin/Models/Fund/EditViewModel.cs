using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.Fund
{
    public class EditViewModel
    {
        [DisplayName("Fund code")]
        [Required(ErrorMessage = "Fund is required")]
        [StringLength(2, ErrorMessage = "Fund cannot be longer than 2 characters")]
        public string FundCode { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, ErrorMessage = "Fund name cannot be longer than 50 characters")]
        public string FundName { get; set; }
        
        [DisplayName("Account reference validator")]
        public int? AccountReferenceValidatorId { get; set; }

        [DisplayName("VAT code")]
        [Required(ErrorMessage = "VAT code is required")]
        public string VatCode { get; set; }

        [DisplayName("Maximum amount")]
        [Required(ErrorMessage = "Maximum amount is required")]
        public decimal? MaximumAmount { get; set; }

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