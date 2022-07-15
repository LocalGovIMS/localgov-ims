using BusinessLogic.Enums;

namespace BusinessLogic.Models.ImportType
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public ImportDataTypeEnum? DataType { get; set; }  
        public string Name { get; set; }
        public string ExternalReference { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}