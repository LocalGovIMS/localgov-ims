using System.ComponentModel.DataAnnotations;

namespace PaymentPortal.Models.Payment
{
    public class EmailReceiptViewModel
    {
        [Display(Name = "Please enter the recipients e-mail address")]
        public string EmailAddress { get; set; }
        public string PspReference { get; set; }
        public string Hash { get; set; }
    }
}