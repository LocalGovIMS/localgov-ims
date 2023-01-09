using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public partial class FileImportRow
    {
        public int Id { get; set; }

        [Required]
        public int FileImportId { get; set; }
        public FileImport FileImport { get; set; }

        [Required]
        public string Data { get; set; }
    }
}
