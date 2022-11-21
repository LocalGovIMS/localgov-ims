using BusinessLogic.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

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

        public SuspenseAllocationStatusEnum Status { get; set; }

        public SelectList Statuses { get; set; }

        public int Page { get; set; }
    }
}