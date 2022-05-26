using System.ComponentModel;

namespace Admin.Models.FundMessage
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        [DisplayName("Fund")]
        public string FundCode { get; set; }

        [DisplayName("Fund")]
        public string FundName { get; set; }

        [DisplayName("Message")]
        public string Message { get; set; }
    }
}