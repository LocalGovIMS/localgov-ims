using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Admin.Models.Suspense
{
    public class DetailsViewModel
    {
        public int? SuspenseId { get; set; }

        [DisplayName("Created Date")]
        public DateTime CreatedAt { get; set; }

        [DisplayName("Account Number")]
        public string AccountNumber { get; set; }

        [DisplayName("Narrative")]
        public string Narrative { get; set; }

        [DisplayName("Notes")]
        public string Notes { get; set; }

        public decimal? Amount { get; set; }

        [DisplayName("Amount Remaining")]
        public decimal? AmountRemaining { get; set; }

        [DisplayName("Amount Allocated")]
        public decimal? AmountAllocated { get; set; }

        public List<BusinessLogic.Entities.SuspenseProcessedTransaction> AllocatedPayments { get; set; }
    }
}