using BusinessLogic.Enums;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.ImportType
{
    public class SearchCriteria
    {
        [Display(Name = "Data type")]
        public ImportDataTypeEnum DataType { get; set; }

        public string Name { get; set; }

        [Display(Name = "External reference")]
        public string ExternalReference { get; set; }

        public int Page { get; set; }
    }
}