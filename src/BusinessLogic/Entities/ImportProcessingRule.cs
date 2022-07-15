namespace BusinessLogic.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class ImportProcessingRule
    {
        public ImportProcessingRule()
        {
            Conditions = new HashSet<ImportProcessingRuleCondition>();
            Actions = new HashSet<ImportProcessingRuleAction>();
            ImportTypes = new HashSet<ImportTypeImportProcessingRule>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public bool IsGlobal { get; set; }

        [Required]
        public bool Disabled { get; set; }

        public virtual ICollection<ImportProcessingRuleCondition> Conditions { get; set; }

        public virtual ICollection<ImportProcessingRuleAction> Actions { get; set; }

        public virtual ICollection<ImportTypeImportProcessingRule> ImportTypes { get; set; }
    }
}
