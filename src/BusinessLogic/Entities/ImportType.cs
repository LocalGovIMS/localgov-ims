namespace BusinessLogic.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ImportType
    {
        public ImportType()
        {
            ImportProcessingRules = new HashSet<ImportTypeImportProcessingRule>();
            Imports = new HashSet<Import>();
        }

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public byte DataType { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [StringLength(100)]
        public string ExternalReference { get; set; }

        public bool IsReversible { get; set; }

        public virtual ICollection<ImportTypeImportProcessingRule> ImportProcessingRules { get; set; }

        public virtual ICollection<Import> Imports { get; set; }
    }
}
