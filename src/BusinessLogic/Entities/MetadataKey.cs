using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public partial class MetadataKey
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public bool SystemType { get; set; }

        public byte EntityType { get; set; }
    }
}
