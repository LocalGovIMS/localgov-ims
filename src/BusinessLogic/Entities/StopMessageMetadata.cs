using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Entities
{
    [Table("StopMessageMetadata")]
    public partial class StopMessageMetadata
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public int StopMessageId { get; set; }

        public virtual StopMessage StopMessage { get; set; }
    }
}
