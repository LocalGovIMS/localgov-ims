using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Entities
{
    [Table("FundMessageMetadata")]
    public partial class FundMessageMetadata
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public int FundMessageId { get; set; }

        public virtual FundMessage FundMessage { get; set; }
    }
}
