using PagedList;

namespace Admin.Models.ImportProcessingRuleCondition
{
    public class ListViewModel
    {
        public StaticPagedList<BusinessLogic.Entities.ImportProcessingRuleCondition> Items { get; set; }
        public int Count { get; set; }
        public int Pages { get; set; }
        public int Page { get; set; }
        public SearchCriteria SearchCriteria { get; set; }
    }
}