using BusinessLogic.Enums;

namespace BusinessLogic.Models.MethodOfPaymentMetadataKey
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public string Name { get; set; }

        public MopMetadataKeyType? Type { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}