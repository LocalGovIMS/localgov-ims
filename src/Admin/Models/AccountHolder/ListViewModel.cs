using PagedList;

namespace Admin.Models.AccountHolder
{
    public class ListViewModel
    {
        public StaticPagedList<BusinessLogic.Entities.AccountHolder> Items { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public SearchViewModel SearchCriteria { get; set; }
        public string FundName { get; set; }
    }
}