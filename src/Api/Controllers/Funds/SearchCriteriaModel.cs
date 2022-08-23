using BusinessLogic.Models.Fund;

namespace Api.Controllers.Funds
{
    public class SearchCriteriaModel
    {
        public string FundCode { get; set; }
        public string FundName { get; set; }
        public bool IsDisabled { get; set; }

        public SearchCriteria ToSearchCriteria()
        {
            return new SearchCriteria
            {
                FundCode = FundCode,
                FundName = FundName,    
                IsDisabled  = IsDisabled,
                ApplyPaging = false
            };
        }
    }
}