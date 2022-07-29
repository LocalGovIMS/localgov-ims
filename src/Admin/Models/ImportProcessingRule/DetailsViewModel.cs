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

        [DisplayName("Global")]
        public bool IsGlobal { get; set; }

        [DisplayName("Status")]
        public bool IsDisabled { get; set; }

        [DisplayName("Status")]
        public string Status => IsDisabled ? "Disabled" : "Active";

        [DisplayName("Scope")]
        public string Scope => IsGlobal ? "Global" : "Specific to the specified import types";

        public List<ImportProcessingRuleCondition.DetailsViewModel> Conditions { get; set; }
    }
}