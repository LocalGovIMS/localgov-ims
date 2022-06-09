using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.TransactionImportTypeImportProcessingRule
{
    public class EditViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Transaction import type")]
        public int TransactionImportTypeId { get; set; }

        [DisplayName("Transaction import type")]
        public string TransactionImportTypeName { get; set; }

        [Required]
        [DisplayName("Import processing rule")]
        public int ImportProcessingRuleId { get; set; }

        public SelectList ImportProcessingRules { get; set; }
    }
}