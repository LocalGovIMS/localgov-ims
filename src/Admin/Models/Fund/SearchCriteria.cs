using System.ComponentModel;

namespace Admin.Models.Fund
{
    public class SearchCriteria
    {
        [DisplayName("Code")]
        public string FundCode { get; set; }

        [DisplayName("Name")]
        public string FundName { get; set; }

        public int Page { get; set; }
    }
}