using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Entities
{
    [Table("VatMetadata")]
    public partial class VatMetadata
    {
        public int Id { get; set; }

        [Required]
        public int MetadataKeyId { get; set; }

        public virtual MetadataKey MetadataKey { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        [StringLength(5)]
        public string VatCode { get; set; }

        public virtual Vat Vat { get; set; }
    }
}
