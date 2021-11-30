using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public partial class ImportProcessingRuleOperator
    {
        public ImportProcessingRuleOperator()
        {
            Conditions = new HashSet<ImportProcessingRuleCondition>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string DisplayName { get; set; }

        [Required]
        [StringLength(50)]
        public string Type { get; set; }

        [Required]
        public int DisplayOrder { get; set; }

        public virtual ICollection<ImportProcessingRuleCondition> Conditions { get; set; }
    }
}
