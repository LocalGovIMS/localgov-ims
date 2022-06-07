namespace BusinessLogic.Models.TransactionImportType
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public string Name { get; set; }
        public string ExternalReference { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}