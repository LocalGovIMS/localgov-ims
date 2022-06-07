using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.TransactionImportType
{
    public class EditViewModel
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("External reference")]
        public string ExternalReference { get; set; }

        public bool ImportProcessingRulesAreAvailableToAdd { get; set; }
    }
}