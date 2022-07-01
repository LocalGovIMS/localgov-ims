namespace BusinessLogic.Models.ImportProcessingRule
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public string Name { get; set; }

        public bool? IsGlobal { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}