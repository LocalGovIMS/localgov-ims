using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.SystemMessage
{
    public class EditViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(160)]
        public string Message { get; set; }

        [Required]
        [RegularExpression("^info$|^warning$|^error$", ErrorMessage = "The severity is not valid")]
        public string Severity { get; set; }

        [Required(ErrorMessage = "Start date is required")]
        [DisplayName("Start date")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "End date is required")]
        [DisplayName("End date")]
        public DateTime EndDate { get; set; }

        public SelectList SeverityList { get; set; }
    }
}