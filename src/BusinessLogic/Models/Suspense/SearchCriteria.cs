using System;

namespace BusinessLogic.Models.Suspense
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public DateTime? CreatedAtDateFrom { get; set; }
        public DateTime? CreatedAtDateTo { get; set; }
        public string AccountNumber { get; set; }
        public string Narrative { get; set; }
        public string BatchReference { get; set; }
        public decimal? Amount { get; set; }
        public bool ShowAllocated { get; set; }

        public SearchCriteria() : base()
        {
            Page = 1;
            PageSize = 6;
        }
    }
}