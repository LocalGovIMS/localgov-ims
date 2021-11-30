using System.ComponentModel;
using Web.Mvc;

namespace Admin.Models.ImportProcessingRuleCondition
{
    public class EditViewModel
    {
        public int Id { get; set; }

        public int Group { get; set; }

        // Note: This will be used to determine whether or not to show the logical operator field - as that field is only relevant for items 2 and above in a group       
        public bool IsFirstItemInTheGroup { get; set; }

        public int ImportProcessingRuleId { get; set; }

        [DisplayName("Field")]
        public int ImportProcessingRuleFieldId { get; set; }

        [DisplayName("Operator")]
        public int ImportProcessingRuleOperatorId { get; set; }

        [DisplayName("Value")]
        public string Value { get; set; }

        [DisplayName("Logical Operator")]
        public string LogicalOperator { get; set; }

        public SelectList LogicalOperators { get; set; }

        public SelectList Fields { get; set; }

        public SelectList Operators { get; set; }
    }
}