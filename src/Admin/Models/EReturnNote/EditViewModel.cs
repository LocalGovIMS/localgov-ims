using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.EReturnNote
{
    public class EditViewModel
    {
        [DisplayName("Note")]
        [Required(ErrorMessage = "Note is required")]
        public string Note { get; set; }

        public int EReturnId { get; set; }
    }
}