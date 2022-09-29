using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public partial class MopMetadataKey
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public byte Type { get; set; }
    }
}
