using BusinessLogic.Enums;

namespace BusinessLogic.Models.MetadataKey
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public string Name { get; set; }

        public bool? SystemType { get; set; }

        public MetadataKeyEntityType? EntityType { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}