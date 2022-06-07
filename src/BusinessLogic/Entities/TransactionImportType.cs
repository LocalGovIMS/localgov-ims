namespace BusinessLogic.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class TransactionImportType
    {
        public TransactionImportType()
        {
            ImportProcessingRules = new HashSet<TransactionImportTypeImportProcessingRule>();
        }

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(100)]
        public string ExternalReference { get; set; }

        public virtual ICollection<TransactionImportTypeImportProcessingRule> ImportProcessingRules { get; set; }
    }
}
