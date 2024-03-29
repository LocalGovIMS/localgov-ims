namespace BusinessLogic.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Suspense
    {
        public Suspense()
        {
            SuspenseProcessedTransactions = new HashSet<SuspenseProcessedTransaction>();
            SuspenseNotes = new HashSet<SuspenseNote>();
        }

        public int Id { get; set; }

        public DateTime TransactionDate { get; set; }

        public DateTime CreatedAt { get; set; }

        [StringLength(50)]
        public string AccountNumber { get; set; }

        [StringLength(100)]
        public string Narrative { get; set; }

        public decimal Amount { get; set; }

        public int? ImportId { get; set; }

        public Import Import { get; set; }

        [StringLength(36)]
        public string ProcessId { get; set; }

        [StringLength(250)]
        public string Notes { get; set; }

        public virtual ICollection<SuspenseProcessedTransaction> SuspenseProcessedTransactions { get; set; }

        public virtual ICollection<SuspenseNote> SuspenseNotes { get; set; }
    }
}
