using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public partial class ImportRow
    {
        public int Id { get; set; }

        [Required]
        public int ImportId { get; set; }
        public Import Import { get; set; }

        [Required]
        public string RowData { get; set; } // TODO: Rename to Data
    }
}
