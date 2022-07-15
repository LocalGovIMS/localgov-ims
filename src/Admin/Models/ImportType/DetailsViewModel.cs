using BusinessLogic.Enums;
using System.ComponentModel;

namespace Admin.Models.ImportType
{
    public class DetailsViewModel
    { 
        public int Id { get; set; }

        [DisplayName("Data type")]
        public ImportDataTypeEnum DataType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        [DisplayName("External reference")]
        public string ExternalReference { get; set; }

        [DisplayName("Is reversible")]
        public bool IsReversible { get; set; }
    }
}