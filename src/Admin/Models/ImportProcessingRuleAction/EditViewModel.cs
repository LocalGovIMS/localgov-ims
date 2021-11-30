using System.ComponentModel;
using Web.Mvc;

namespace Admin.Models.ImportProcessingRuleAction
{
    public class EditViewModel
    {
        public int Id { get; set; }

        public int ImportProcessingRuleId { get; set; }

        [DisplayName("Field")]
        public int ImportProcessingRuleFieldId { get; set; }

        [DisplayName("Value")]
        public string Value { get; set; }

        public SelectList Fields { get; set; }
    }
}