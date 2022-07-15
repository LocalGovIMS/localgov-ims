using BusinessLogic.Enums;

namespace Admin.Models.ImportType
{
    public class SearchCriteria
    {
        public ImportDataTypeEnum DataType { get; set; }
        public string Name { get; set; }
        public string ExternalReference { get; set; }
        public int Page { get; set; }
    }
}