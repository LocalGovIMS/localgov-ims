using PagedList;

namespace Admin.Models.ImportProcessingRuleAction
{
    public class ListViewModel
    {
        public StaticPagedList<BusinessLogic.Entities.ImportProcessingRuleAction> Items { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public SearchCriteria SearchCriteria { get; set; }
    }
}