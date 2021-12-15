namespace BusinessLogic.Models.FundMetadata
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public string FundCode { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}