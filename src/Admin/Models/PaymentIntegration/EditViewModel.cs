using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.PaymentIntegration
{
    public class EditViewModel
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Base URI")]
        public string BaseUri { get; set; }
    }
}