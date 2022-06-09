using System.ComponentModel;

namespace Admin.Models.ImportProcessingRuleTransactionImportType
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        [DisplayName("Transaction import type")]
        public int TransactionImportTypeId { get; set; }

        [DisplayName("Transaction import type")]
        public string TransactionImportTypeName { get; set; }

        [DisplayName("Import processing rule")]
        public int ImportProcessingRuleId { get; set; }

        [DisplayName("Import processing rule")]
        public string ImportProcessingRuleName { get; set; }
    }
}