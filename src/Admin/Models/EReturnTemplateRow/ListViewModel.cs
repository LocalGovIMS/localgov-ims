using PagedList;

namespace Admin.Models.EReturnTemplateRow
{
    public class ListViewModel
    {
        public StaticPagedList<DetailsViewModel> Items { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public SearchCriteria SearchCriteria { get; set; }
    }
}