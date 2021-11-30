namespace BusinessLogic.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Office
    {
        public Office()
        {
            PendingTransactions = new HashSet<PendingTransaction>();
            ProcessedTransactions = new HashSet<ProcessedTransaction>();
            Users = new HashSet<User>();
        }

        [Key]
        [StringLength(2)]
        public string OfficeCode { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public virtual ICollection<PendingTransaction> PendingTransactions { get; set; }

        public virtual ICollection<ProcessedTransaction> ProcessedTransactions { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
