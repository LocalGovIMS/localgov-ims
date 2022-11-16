using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Enums
{
    public enum MetadataKeyEntityType
    {
        Mop = 1,
        Fund = 2,

        [Display(Name = "Fund message")]
        FundMessage = 3,

        [Display(Name = "VAT")]
        Vat = 4,
        Import = 5
    }
}