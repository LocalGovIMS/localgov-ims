using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.Role
{
    public class EditViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayName("Display Name")]
        [Required(ErrorMessage = "Display Name is required")]
        public string DisplayName { get; set; }
    }
}