using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessLogic.Entities
{
    [Table("VatMetaData")]
    public partial class VatMetaData
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Key { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        [StringLength(5)]
        public string VatCode { get; set; }

        public virtual Vat Vat { get; set; }
    }
}
