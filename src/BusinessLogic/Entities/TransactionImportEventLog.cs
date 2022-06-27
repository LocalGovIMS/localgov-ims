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
        
        public DateTime CreatedDate { get; set; }

        [Required]
        public int TransactionImportId { get; set; }
        public virtual TransactionImport TransactionImport { get; set; }
        
        public byte Type { get; set; }

        public string Message { get; set; }
    }
}
