using BusinessLogic.Models.MethodOfPayment;

namespace Api.Controllers.MethodOfPayments
{
    public class SearchCriteriaModel
    {
        public string Type { get; set; }

        public SearchCriteria ToSearchCriteria()
        {
            return new SearchCriteria
            {
                Type = Type
            };
        }
    }
}