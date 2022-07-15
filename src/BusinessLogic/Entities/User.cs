namespace BusinessLogic.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class User
    {
        public User()
        {
            ApprovedEReturns = new HashSet<EReturn>();
            CreatedEReturns = new HashSet<EReturn>();
            SubmittedEReturns = new HashSet<EReturn>();
            PendingTransactions = new HashSet<PendingTransaction>();
            ProcessedTransactions = new HashSet<ProcessedTransaction>();
            SuspenseNotes = new HashSet<SuspenseNote>();
            SuspenseProcessedTransactions = new HashSet<SuspenseProcessedTransaction>();
            UserFundGroups = new HashSet<UserFundGroup>();
            UserMethodOfPayments = new HashSet<UserMethodOfPayment>();
            UserRoles = new HashSet<UserRole>();
            UserTemplates = new HashSet<UserTemplate>();
            Imports = new HashSet<Import>();
            ImportStatusHistories = new HashSet<ImportStatusHistory>();
        }

        public int UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string UserName { get; set; }

        public DateTime? LastLogin { get; set; }

        public int ExpiryDays { get; set; }

        public bool Disabled { get; set; }

        [StringLength(150)]
        public string DisplayName { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? LastEnabledAt { get; set; }

        [StringLength(2)]
        public string OfficeCode { get; set; }

        public virtual Office Office { get; set; }

        public virtual ICollection<EReturn> ApprovedEReturns { get; set; }

        public virtual ICollection<EReturn> CreatedEReturns { get; set; }

        public virtual ICollection<EReturn> SubmittedEReturns { get; set; }

        public virtual ICollection<PendingTransaction> PendingTransactions { get; set; }

        public virtual ICollection<ProcessedTransaction> ProcessedTransactions { get; set; }

        public virtual ICollection<SuspenseNote> SuspenseNotes { get; set; }

        public virtual ICollection<SuspenseProcessedTransaction> SuspenseProcessedTransactions { get; set; }

        public virtual ICollection<UserFundGroup> UserFundGroups { get; set; }

        public virtual ICollection<UserMethodOfPayment> UserMethodOfPayments { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }

        public virtual ICollection<UserTemplate> UserTemplates { get; set; }
        
        public virtual ICollection<Import> Imports { get; set; }

        public virtual ICollection<ImportStatusHistory> ImportStatusHistories { get; set; }
    }
}
