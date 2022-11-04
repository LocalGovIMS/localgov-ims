namespace BusinessLogic.Models.AccountHolder
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public string AccountReference { get; set; }
        public string Address { get; set; }
        public string PostCode { get; set; }
        public string Surname { get; set; }
        public string FundCode { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}