using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public partial class ImportProcessingRuleField
    {
        public ImportProcessingRuleField()
        {
            Conditions = new HashSet<ImportProcessingRuleCondition>();
            Actions = new HashSet<ImportProcessingRuleAction>();
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

        public virtual ICollection<ImportProcessingRuleAction> Actions { get; set; }
    }
}
