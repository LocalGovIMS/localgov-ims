using PagedList;

namespace Admin.Models.Transaction
{
    public class ListViewModel
    {
        public StaticPagedList<BusinessLogic.Models.Transactions.SearchResultItem> Transactions { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public SearchCriteria SearchCriteria { get; set; }
    }
}