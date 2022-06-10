namespace BusinessLogic.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TransactionImportRow
    {
        public TransactionImportRow()
        {
            EventLog = new HashSet<TransactionImportEventLog>();
        }

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int TransactionImportId { get; set; }
        public virtual TransactionImport TransactionImport { get; set; }

        public string Data { get; set; }

        public virtual ICollection<TransactionImportEventLog> EventLog { get; set; }
    }
}
