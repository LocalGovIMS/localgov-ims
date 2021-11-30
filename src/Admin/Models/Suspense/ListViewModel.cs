using BusinessLogic.Models.Suspense;
using PagedList;

namespace Admin.Models.Suspense
{
    public class ListViewModel
    {
        public StaticPagedList<SuspenseWrapper> Items { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public SearchCriteria SearchCriteria { get; set; }
    }
}