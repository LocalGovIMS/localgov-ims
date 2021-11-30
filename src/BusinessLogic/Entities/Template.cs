namespace BusinessLogic.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Template
    {
        public Template()
        {
            EReturns = new HashSet<EReturn>();
            TemplateRows = new HashSet<TemplateRow>();
            UserTemplates = new HashSet<UserTemplate>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        public bool ModifyVat { get; set; }

        public bool ModifyReference { get; set; }

        public bool ModifyDescription { get; set; }

        public bool Cheque { get; set; }

        public bool Cash { get; set; }

        public bool Pdq { get; set; }

        public virtual ICollection<EReturn> EReturns { get; set; }

        public virtual ICollection<TemplateRow> TemplateRows { get; set; }

        public virtual ICollection<UserTemplate> UserTemplates { get; set; }
    }
}
