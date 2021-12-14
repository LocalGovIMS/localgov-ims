namespace BusinessLogic.Models.MethodOfPaymentMetadata
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public string MopCode { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}