using System;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.Suspense
{
    public class SearchCriteria
    {
        [Display(Name = "Start date")]
        public DateTime? TransactionDateFrom { get; set; }

        [Display(Name = "End date")]
        public DateTime? TransactionDateTo { get; set; }

        [Display(Name = "Account number")]
        public string AccountNumber { get; set; }

        public string Narrative { get; set; }

        public decimal? Amount { get; set; }

        [Display(Name = "Show all")]
        public bool ShowAll { get; set; }

        public int Page { get; set; }
    }
}