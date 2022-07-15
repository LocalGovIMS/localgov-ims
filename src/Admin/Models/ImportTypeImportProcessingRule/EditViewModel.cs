using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.ImportTypeImportProcessingRule
{
    public class EditViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Import type")]
        public int ImportTypeId { get; set; }

        [DisplayName("Import type")]
        public string ImportTypeName { get; set; }

        [Required]
        [DisplayName("Import processing rule")]
        public int ImportProcessingRuleId { get; set; }

        public SelectList ImportProcessingRules { get; set; }
    }
}