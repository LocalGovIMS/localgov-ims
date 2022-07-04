using System;

namespace Admin.Models.Suspense
{
    public class SearchCriteria
    {
        public DateTime? TransactionDateFrom { get; set; }
        public DateTime? TransactionDateTo { get; set; }
        public string AccountNumber { get; set; }
        public string Narrative { get; set; }
        public decimal? Amount { get; set; }
        public bool ShowAll { get; set; }
        public int Page { get; set; }
    }
}