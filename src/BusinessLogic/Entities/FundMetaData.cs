using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Entities
{
    [Table("FundMetadata")]
    public partial class FundMetadata
    {
        public int Id { get; set; }

        [Required]
        public int MetadataKeyId { get; set; }

        public virtual MetadataKey MetadataKey { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        [StringLength(5)]
        public string FundCode { get; set; }

        public virtual Fund Fund { get; set; }
    }
}
