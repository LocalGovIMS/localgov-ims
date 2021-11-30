namespace BusinessLogic.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public partial class SystemMessage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(10)]
        public string Severity { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Required]
        [StringLength(160)]
        public string Message { get; set; }
    }
}
