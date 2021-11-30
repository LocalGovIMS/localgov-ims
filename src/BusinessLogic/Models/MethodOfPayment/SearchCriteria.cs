namespace BusinessLogic.Models.MethodOfPayment
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public string Type { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}