using System.Collections.Generic;
using System.ComponentModel;

namespace Admin.Models.ImportProcessingRule
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Status")]
        public bool IsDisabled { get; set; }

        [DisplayName("Status")]
        public string Status => IsDisabled ? "Disabled" : "Active";

        public List<ImportProcessingRuleCondition.DetailsViewModel> Conditions { get; set; }
    }
}