using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.SystemMessage
{
    public class DetailsViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        public string Severity { get; set; }

        public string SeverityDisplayName { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DisplayName("End date")]
        public DateTime EndDate { get; set; }

        public bool IsActive
        {
            get { return StartDate < DateTime.Now && EndDate > DateTime.Now; }
        }
    }
}