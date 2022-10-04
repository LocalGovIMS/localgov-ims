using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.FundMessageMetadata
{
    public class EditViewModel
    {
        public int Id { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Key")]
        [Required]
        public int MetadataKeyId { get; set; }

        [DisplayName("Key")]
        public string MetadataKeyName { get; set; }

        [DisplayName("Description")]
        public string MetadataKeyDescription { get; set; }

        [DisplayName("Value")]
        [Required]
        public string Value { get; set; }

        [DisplayName("Fund Message")]
        public int FundMessageId { get; set; }

        [DisplayName("Fund Message")]
        public string FundMessage { get; set; }

        public SelectList MetadataKeys { get; set; }
    }
}