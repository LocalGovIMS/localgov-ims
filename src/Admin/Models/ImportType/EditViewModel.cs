using BusinessLogic.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.ImportType
{
    public class EditViewModel
    {
        [Required(ErrorMessage = "Id is required")]
        public int Id { get; set; }

        [DisplayName("Data type")]
        public ImportDataTypeEnum DataType { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("External reference")]
        public string ExternalReference { get; set; }

        [DisplayName("Is reversible")]
        public bool IsReversible { get; set; }

        public bool ImportProcessingRulesAreAvailableToAdd { get; set; }

        public SelectList ImportTypes { get; set; }
    }
}