namespace BusinessLogic.Models.ImportTypeImportProcessingRule
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public int? ImportTypeId { get; set; }
        public int? ImportProcessingRuleId { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}