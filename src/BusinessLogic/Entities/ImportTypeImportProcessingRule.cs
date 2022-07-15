using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Entities
{
    public partial class ImportTypeImportProcessingRule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int ImportTypeId { get; set; }
        public virtual ImportType ImportType { get; set; }

        [Required]
        public int ImportProcessingRuleId { get; set; }
        public virtual ImportProcessingRule ImportProcessingRule { get; set; }
    }
}
