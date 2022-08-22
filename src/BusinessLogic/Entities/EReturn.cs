namespace BusinessLogic.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class EReturn
    {
        public EReturn()
        {
            EReturnCashes = new HashSet<EReturnCash>();
            EReturnCheques = new HashSet<EReturnCheque>();
            PendingTransactions = new HashSet<PendingTransaction>();
            ProcessedTransactions = new HashSet<ProcessedTransaction>();
            Notes = new HashSet<EReturnNote>();
        }

        public int Id { get; set; }

        [StringLength(13)]
        public string EReturnNo { get; set; }

        public DateTime? ApprovedAt { get; set; }

        public int? ApprovedByUserId { get; set; }

        public int TypeId { get; set; }

        public int StatusId { get; set; }

        public int TemplateId { get; set; }

        public DateTime CreatedAt { get; set; }

        public int? CreatedByUserId { get; set; }

        public int? SubmittedByUserId { get; set; }

        public DateTime? SubmittedAt { get; set; }

        [StringLength(36)]
        public string ProcessId { get; set; }

        public virtual EReturnStatus EReturnStatus { get; set; }

        public virtual EReturnType EReturnType { get; set; }

        public virtual Template Template { get; set; }

        public virtual User ApprovedByUser { get; set; }

        public virtual User CreatedByUser { get; set; }

        public virtual User SubmittedByUser { get; set; }

        public virtual ICollection<EReturnCash> EReturnCashes { get; set; }

        public virtual ICollection<EReturnCheque> EReturnCheques { get; set; }

        public virtual ICollection<PendingTransaction> PendingTransactions { get; set; }

        public virtual ICollection<ProcessedTransaction> ProcessedTransactions { get; set; }

        public virtual ICollection<EReturnNote> Notes { get; set; }
    }
}
