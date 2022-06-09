using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.ImportProcessingRuleTransactionImportType
{
    public class EditViewModel
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Transaction import type")]
        public int TransactionImportTypeId { get; set; }

        [Required]
        [DisplayName("Import processing rule")]
        public int ImportProcessingRuleId { get; set; }

        [DisplayName("Import processing rule")]
        public string ImportProcessingRuleName { get; set; }

        public SelectList TransactionImportTypes { get; set; }
    }
}