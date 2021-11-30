namespace BusinessLogic.Entities
{
    using System;

    public partial class SuspenseProcessedTransaction
    {
        public int Id { get; set; }

        public int SuspenseId { get; set; }

        public int TransactionInId { get; set; }

        public int TransactionOutId { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreatedAt { get; set; }

        public int CreatedByUserId { get; set; }

        public virtual Suspense Suspense { get; set; }

        public virtual ProcessedTransaction TransactionIn { get; set; }

        public virtual ProcessedTransaction TransactionOut { get; set; }

        public virtual User CreatedByUser { get; set; }
    }
}
