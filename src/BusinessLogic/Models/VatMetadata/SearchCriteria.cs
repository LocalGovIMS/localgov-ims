namespace BusinessLogic.Models.VatMetadata
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public string VatCode { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}