using System.ComponentModel;

namespace Admin.Models.Fund
{
    public class SearchCriteria
    {
        [DisplayName("Fund")]
        public string FundCode { get; set; }
        public string FundName { get; set; }
        public int Page { get; set; }
    }
}