using System.ComponentModel;

namespace Admin.Models.ImportProcessingRuleAction
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        public int ImportProcessingRuleId { get; set; }

        [DisplayName("Field")]
        public string FieldName { get; set; }

        [DisplayName("Value")]
        public string Value { get; set; }
    }
}