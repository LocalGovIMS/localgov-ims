namespace BusinessLogic.Models.Fund
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public string FundCode { get; set; }
        public string FundName { get; set; }
        public bool? IsDisabled { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}