using System.ComponentModel;

namespace Admin.Models.ImportProcessingRuleImportType
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        [DisplayName("Import type")]
        public int ImportTypeId { get; set; }

        [DisplayName("Import type")]
        public string ImportTypeName { get; set; }

        [DisplayName("Import processing rule")]
        public int ImportProcessingRuleId { get; set; }

        [DisplayName("Import processing rule")]
        public string ImportProcessingRuleName { get; set; }
    }
}