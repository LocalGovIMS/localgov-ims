using System.ComponentModel;

namespace Admin.Models.VatMetadata
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Key")]
        public int MetadataKeyId { get; set; }

        [DisplayName("Key")]
        public string MetadataKeyName { get; set; }

        [DisplayName("Description")]
        public string MetadataKeyDescription { get; set; }

        [DisplayName("Value")]
        public string Value { get; set; }

        [DisplayName("Vat Code")]
        public string VatCode { get; set; }
    }
}