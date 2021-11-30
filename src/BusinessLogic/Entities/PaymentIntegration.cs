using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Entities
{
    public partial class PaymentIntegration
    {
        public PaymentIntegration() { }

        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(200)]
        public string BaseUri { get; set; }
    }
}
