using System.ComponentModel;

namespace Admin.Models.ImportProcessingRuleCondition
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        public int ImportProcessingRuleId { get; set; }

        [DisplayName("Field")]
        public string FieldName { get; set; }

        [DisplayName("Operator")]
        public string OperatorName { get; set; }

        [DisplayName("Value")]
        public string Value { get; set; }

        [DisplayName("Logical Operator")]
        public string LogicalOperator { get; set; }
    }
}