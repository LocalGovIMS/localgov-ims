using System;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.Payment
{
    [Serializable]
    public class Cheques
    {
        [Required(ErrorMessage = "Bank account number is required")]
        [Display(Name = "Bank account number")]
        [MaxLength(8, ErrorMessage = "Bank account number must be 8 digits")]
        [MinLength(8, ErrorMessage = "Bank account number must be 8 digits")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Bank account number must be numeric")]
        public string BankAccountNo { get; set; }

        [Required(ErrorMessage = "Sort code is required")]
        [Display(Name = "Sort code")]
        [MaxLength(6, ErrorMessage = "Sort code must be 6 digits")]
        [MinLength(6, ErrorMessage = "Sort code must be 6 digits")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Sort code must be numeric")]
        public string SortCode { get; set; }

        [Required(ErrorMessage = "Cheque number is required")]
        [Display(Name = "Cheque number")]
        [MaxLength(6, ErrorMessage = "Cheque number must be 6 digits")]
        [MinLength(6, ErrorMessage = "Cheque number must be 6 digits")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Cheque number must be numeric")]
        public string ChequeNumber { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [Display(Name = "Name on the cheque")]
        public string Name { get; set; }
    }
}