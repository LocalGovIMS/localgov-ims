using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Entities
{
    [Table("MopMetadata")]
    public partial class MopMetadata
    {
        public int Id { get; set; }

        [Required]
        public int MetadataKeyId { get; set; }

        public virtual MetadataKey MetadataKey { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        [StringLength(5)]
        public string MopCode { get; set; }

        public virtual Mop Mop { get; set; }
    }
}
