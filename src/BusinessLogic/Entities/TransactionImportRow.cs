namespace BusinessLogic.Entities
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TransactionImportRow
    {
        public TransactionImportRow()
        {
        }

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int TransactionImportId { get; set; }
        public virtual TransactionImport TransactionImport { get; set; }

        public string Data { get; set; }
    }
}
