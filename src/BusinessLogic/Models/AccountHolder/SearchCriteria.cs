namespace BusinessLogic.Models.AccountHolder
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public string AccountReference { get; set; }
        public string HouseNumberName { get; set; }
        public string Street { get; set; }
        public string PostCode { get; set; }
        public string Surname { get; set; }
        public string FundCode { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}