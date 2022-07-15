using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public partial class FileImport
    {
        public FileImport()
        {
            Rows = new HashSet<FileImportRow>();
        }

        public int Id { get; set; }

        [Required]
        public int ImportId { get; set; }
        public virtual Import Import { get; set; }

        [Required]
        [StringLength(200)]
        public string OriginalFilename { get; set; }

        [Required]
        [StringLength(200)]
        public string WorkingFilename { get; set; }

        public virtual ICollection<FileImportRow> Rows { get; set; }
    }
}
