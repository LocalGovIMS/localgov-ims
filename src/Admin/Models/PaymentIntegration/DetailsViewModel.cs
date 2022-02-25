using System.ComponentModel;

namespace Admin.Models.PaymentIntegration
{
    public class DetailsViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [DisplayName("Base URI")]
        public string BaseUri { get; set; }
    }
}