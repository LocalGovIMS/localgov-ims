using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Entities
{
    [Table("TransactionImportTypeImportProcessingRule")]
    public partial class TransactionImportTypeImportProcessingRule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int TransactionImportTypeId { get; set; }
        public virtual TransactionImportType TransactionImportType { get; set; }

        [Required]
        public int ImportProcessingRuleId { get; set; }
        public virtual ImportProcessingRule ImportProcessingRule { get; set; }
    }
}
