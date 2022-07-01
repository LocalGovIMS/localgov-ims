namespace BusinessLogic.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TransactionImport
    {
        public TransactionImport()
        {
            Rows = new HashSet<TransactionImportRow>();
            ProcessedTransactions = new HashSet<ProcessedTransaction>();
            FileImports = new HashSet<FileImport>();
            StatusHistories = new HashSet<TransactionImportStatusHistory>();
            EventLogs = new HashSet<TransactionImportEventLog>();
        }

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TransactionImportTypeId { get; set; }
        public virtual TransactionImportType TransactionImportType { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }

        [StringLength(100)]
        public string ExternalReference { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal TotalNumberOfTransactions { get; set; } // TODO: Change to int

        public DateTime? ReversedDate { get; set; }

        public virtual ICollection<TransactionImportRow> Rows { get; set; }
        public virtual ICollection<ProcessedTransaction> ProcessedTransactions { get; set; }
        public virtual ICollection<FileImport> FileImports { get; set; }
        public virtual ICollection<TransactionImportStatusHistory> StatusHistories { get; set; }
        public virtual ICollection<TransactionImportEventLog> EventLogs { get; set; }
    }
}
