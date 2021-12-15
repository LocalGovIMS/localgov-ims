using System.ComponentModel;

namespace Admin.Models.FundMetadata
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Key")]
        public string Key { get; set; }

        [DisplayName("Value")]
        public string Value { get; set; }

        [DisplayName("Fund Code")]
        public string FundCode { get; set; }
    }
}