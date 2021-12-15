using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.VatMetadata
{
    public class EditViewModel
    {
        public int Id { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Key")]
        [Required]
        public string Key { get; set; }

        [DisplayName("Value")]
        [Required]
        public string Value { get; set; }

        [DisplayName("Vat Code")]
        public string VatCode { get; set; }

        public SelectList Keys { get; set; }
    }
}