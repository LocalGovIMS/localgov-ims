using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.SuspenseNote
{
    public class EditViewModel
    {
        [DisplayName("Note")]
        [Required(ErrorMessage = "Note is required")]
        public string Note { get; set; }

        public int SuspenseId { get; set; }
    }
}