using BusinessLogic.Models.Suspense;
using System;

namespace Api.Controllers.Suspense
{
    public class SearchCriteriaModel
    {
        public DateTime? CreatedAtDateFrom { get; set; }
        public DateTime? CreatedAtDateTo { get; set; }

        public SearchCriteria ToSearchCriteria()
        {
            return new SearchCriteria
            {
                CreatedAtDateFrom = CreatedAtDateFrom,
                CreatedAtDateTo = CreatedAtDateTo,
            };
        }
    }
}