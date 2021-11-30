namespace BusinessLogic.Entities
{
    using System.ComponentModel.DataAnnotations;

    public partial class TemplateRow
    {
        public int Id { get; set; }

        public int TemplateId { get; set; }

        [Required]
        [StringLength(50)]
        public string Reference { get; set; }

        [Required]
        [StringLength(2)]
        public string VatCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Description { get; set; }

        public bool VatOverride { get; set; }

        public bool ReferenceOverride { get; set; }

        public bool DescriptionOverride { get; set; }

        public virtual Template Template { get; set; }

        public virtual Vat VAT { get; set; }
    }
}
