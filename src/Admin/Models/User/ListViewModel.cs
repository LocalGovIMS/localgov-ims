using PagedList;

namespace Admin.Models.User
{
    public class ListViewModel
    {
        public StaticPagedList<BusinessLogic.Entities.User> Items { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public SearchCriteria SearchCriteria { get; set; }
    }
}