namespace BusinessLogic.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Import
    {
        public Import()
        {
            Rows = new HashSet<ImportRow>();
            ProcessedTransactions = new HashSet<ProcessedTransaction>();
            SuspenseItems = new HashSet<Suspense>();
            FileImports = new HashSet<FileImport>();
            StatusHistories = new HashSet<ImportStatusHistory>();
            EventLogs = new HashSet<ImportEventLog>();
        }

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int ImportTypeId { get; set; }
        public virtual ImportType ImportType { get; set; }

        public DateTime CreatedDate { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }

        [StringLength(255)]
        public string Notes { get; set; }

        public int NumberOfRows { get; set; }

        public DateTime? ReversedDate { get; set; } // TODO: How does this work for AccountHolder imports? Does type need to have a 'Reversible' flag?

        public virtual ICollection<ImportRow> Rows { get; set; }
        public virtual ICollection<ProcessedTransaction> ProcessedTransactions { get; set; }
        public virtual ICollection<Suspense> SuspenseItems { get; set; }
        public virtual ICollection<FileImport> FileImports { get; set; }
        public virtual ICollection<ImportStatusHistory> StatusHistories { get; set; }
        public virtual ICollection<ImportEventLog> EventLogs { get; set; }
    }
}
