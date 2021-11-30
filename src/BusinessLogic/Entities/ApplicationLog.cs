using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public partial class ApplicationLog
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [StringLength(255)]
        public string Thread { get; set; }

        [StringLength(50)]
        public string Level { get; set; }

        [StringLength(255)]
        public string Logger { get; set; }

        [StringLength(500)]
        public string Location { get; set; }

        [StringLength(4000)]
        public string Message { get; set; }

        public string Exception { get; set; }
        
        [StringLength(255)]
        public string HostName { get; set; }

        [StringLength(255)]
        public string ApplicationName { get; set; }
    }
}