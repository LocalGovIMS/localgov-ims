namespace BusinessLogic.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Mop
    {
        public Mop()
        {
            PendingTransactions = new HashSet<PendingTransaction>();
            ProcessedTransactions = new HashSet<ProcessedTransaction>();
            UserMethodOfPayments = new HashSet<UserMethodOfPayment>();
            MetaData = new HashSet<MopMetaData>();
        }

        [Key]
        [StringLength(5)]
        public string MopCode { get; set; }

        [Required]
        [StringLength(30)]
        public string MopName { get; set; }

        public decimal MaximumAmount { get; set; }

        public decimal MinimumAmount { get; set; }

        public bool Disabled { get; set; }

        public virtual ICollection<PendingTransaction> PendingTransactions { get; set; }

        public virtual ICollection<ProcessedTransaction> ProcessedTransactions { get; set; }

        public virtual ICollection<UserMethodOfPayment> UserMethodOfPayments { get; set; }

        public virtual ICollection<MopMetaData> MetaData { get; set; }
    }
}
