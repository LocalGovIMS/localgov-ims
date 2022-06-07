namespace BusinessLogic.Models.TransactionImportTypeImportProcessingRule
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public int? TransactionImportTypeId { get; set; }
        public int? ImportProcessingRuleId { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}