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
        [StringLength(30)]
        public string BatchReference { get; set; }

        [Required]
        [StringLength(200)]
        public string OriginalFilename { get; set; }

        [Required]
        [StringLength(200)]
        public string WorkingFilename { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        [Required]
        public int CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; }

        public virtual ICollection<FileImportRow> Rows { get; set; }
    }
}
