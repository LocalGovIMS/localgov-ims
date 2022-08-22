using PagedList;

namespace Admin.Models.EReturnNote
{
    public class ListViewModel
    {
        public StaticPagedList<BusinessLogic.Entities.EReturnNote> Items { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public int EReturnId { get; set; }
    }
}