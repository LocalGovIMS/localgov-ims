using System.ComponentModel;

namespace Admin.Models.EReturnTemplateRow
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        public int EReturnTemplateId { get; set; }

        public string Reference { get; set; }

        [DisplayName("VAT code")]
        public string VatCode { get; set; }
        
        public string Description { get; set; }

        [DisplayName("VAT override")]
        public bool VatOverride { get; set; }

        [DisplayName("Reference override")]
        public bool ReferenceOverride { get; set; }

        [DisplayName("Description override")]
        public bool DescriptionOverride { get; set; }
    }
}