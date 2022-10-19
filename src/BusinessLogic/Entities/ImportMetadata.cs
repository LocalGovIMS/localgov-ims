using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Entities
{
    [Table("ImportMetadata")]
    public partial class ImportMetadata
    {
        public int Id { get; set; }

        [Required]
        public int MetadataKeyId { get; set; }

        public virtual MetadataKey MetadataKey { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public int ImportId { get; set; }

        public virtual Import Import { get; set; }
    }
}
