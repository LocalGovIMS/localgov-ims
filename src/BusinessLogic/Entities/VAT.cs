namespace BusinessLogic.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Vat")]
    public partial class Vat
    {
        public Vat()
        {
            Funds = new HashSet<Fund>();
            TemplateRows = new HashSet<TemplateRow>();
            PendingTransactions = new HashSet<PendingTransaction>();
            ProcessedTransactions = new HashSet<ProcessedTransaction>();
            MetaData = new HashSet<VatMetaData>();
        }

        [Key]
        [StringLength(2)]
        public string VatCode { get; set; }

        public decimal? Percentage { get; set; }

        public bool Disabled { get; set; }

        public virtual ICollection<Fund> Funds { get; set; }

        public virtual ICollection<TemplateRow> TemplateRows { get; set; }

        public virtual ICollection<PendingTransaction> PendingTransactions { get; set; }

        public virtual ICollection<ProcessedTransaction> ProcessedTransactions { get; set; }

        public virtual ICollection<VatMetaData> MetaData { get; set; }
    }
}
