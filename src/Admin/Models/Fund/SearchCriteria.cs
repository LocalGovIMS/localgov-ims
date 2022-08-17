using System.ComponentModel;

namespace Admin.Models.Fund
{
    public class SearchCriteria
    {
        [DisplayName("Fund code")]
        public string FundCode { get; set; }

        [DisplayName("Fund name")]
        public string FundName { get; set; }

        public int Page { get; set; }
    }
}