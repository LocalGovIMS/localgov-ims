using System.ComponentModel.DataAnnotations;

namespace Admin.Models.Transaction
{
    public class EmailReceiptViewModel
    {
        [Display(Name = "Please enter the recipients e-mail address")]
        public string EmailAddress { get; set; }
        public string PspReference { get; set; }
    }
}