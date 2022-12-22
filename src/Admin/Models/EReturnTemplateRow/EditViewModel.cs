using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.EReturnTemplateRow
{
    public class EditViewModel
    {
        public int Id { get; set; }

        public int EReturnTemplateId { get; set; }

        public string Reference { get; set; }

        [DisplayName("VAT code")]
        public string VatCode { get; set; }

        [Required]
        public string Description { get; set; }

        [DisplayName("VAT override")]
        public bool VatOverride { get; set; }

        [DisplayName("Reference override")]
        public bool ReferenceOverride { get; set; }

        [DisplayName("Description override")]
        public bool DescriptionOverride { get; set; }

        public SelectList VatCodes { get; set; }
    }
}