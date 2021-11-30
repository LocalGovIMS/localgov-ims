using Admin.Models.Shared;
using BusinessLogic.Models;
using PagedList;

namespace Admin.Models.EReturn
{
    public class ListViewModel
    {
        public Message Message { get; set; }
        public StaticPagedList<EReturnWrapper> EReturns { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public SearchCriteria SearchCriteria { get; set; }
    }
}