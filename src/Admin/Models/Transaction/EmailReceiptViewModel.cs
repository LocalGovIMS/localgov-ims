using System.ComponentModel.DataAnnotations;

namespace Admin.Models.Transaction
{
    public class EmailReceiptViewModel
    {
        [Display(Name = "Please enter the recipients email address")]
        public string EmailAddress { get; set; }
        public string PspReference { get; set; }
    }
}