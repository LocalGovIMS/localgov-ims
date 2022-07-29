using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.ImportProcessingRule
{
    public class EditViewModel
    {
        public int Id { get; set; }

        [DisplayName("Name")]
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [DisplayName("Description")]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [DisplayName("Global")]
        public bool IsGlobal { get; set; }

        [DisplayName("Disabled")]
        public bool IsDisabled { get; set; }

        public bool ImportTypesAreAvailableToAdd { get; set; }
    }
}