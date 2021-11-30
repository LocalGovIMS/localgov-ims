using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.EReturn
{
    public class CreateViewModel
    {
        [Required]
        [Display(Name = "Template")]
        public int? TemplateId { get; set; }

        [Required]
        [Display(Name = "Type")]
        public int? TypeId { get; set; }

        public SelectList Templates { get; set; }
        public SelectList Types { get; set; }
    }
}