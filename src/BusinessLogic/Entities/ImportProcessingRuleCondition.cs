using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public partial class ImportProcessingRuleCondition
    {
        public ImportProcessingRuleCondition()
        {
        }

        public int Id { get; set; }

        public int ImportProcessingRuleId { get; set; }

        public int Group { get; set; }

        public int ImportProcessingRuleFieldId { get; set; }

        public int ImportProcessingRuleOperatorId { get; set; }

        public string Value { get; set; }

        [StringLength(3)]
        public string LogicalOperator { get; set; }

        public virtual ImportProcessingRule Rule { get; set; }

        public virtual ImportProcessingRuleField Field { get; set; }

        public virtual ImportProcessingRuleOperator Operator { get; set; }
    }
}
