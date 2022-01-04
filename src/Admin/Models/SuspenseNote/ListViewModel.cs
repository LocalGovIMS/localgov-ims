using PagedList;

namespace Admin.Models.SuspenseNote
{
    public class ListViewModel
    {
        public StaticPagedList<BusinessLogic.Entities.SuspenseNote> Items { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public int SuspenseId{ get; set; }
    }
}