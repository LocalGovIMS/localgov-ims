using System.ComponentModel.DataAnnotations;

namespace Admin.Models.Office
{
    public class EditViewModel
    {
        [Required(ErrorMessage = "Code is required")]
        public string Code { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }
    }
}