namespace BusinessLogic.Models.FundMessage
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public string FundCode { get; set; }
        public string Message { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}