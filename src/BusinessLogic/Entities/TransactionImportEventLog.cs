namespace BusinessLogic.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TransactionImportEventLog
    {
        public TransactionImportEventLog()
        {
        }

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TransactionImportId { get; set; }
        public virtual TransactionImport TransactionImport { get; set; }

        public int? TransactionImportRowId { get; set; }
        public virtual TransactionImportRow TransactionImportRow { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Type { get; set; }

        [StringLength(255)]
        public string Description { get; set; }
    }
}
