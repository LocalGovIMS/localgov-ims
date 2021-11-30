using System;
using System.ComponentModel.DataAnnotations;

namespace PaymentPortal.Models.Payment
{
    [Serializable]
    public class PaymentAddress
    {
        [Required(ErrorMessage = "Payee Name is required")]
        [Display(Name = "Payee Name")]
        [MaxLength(50, ErrorMessage = "Payee Name cannot be longer than 50 characters")]
        public string PayeeName { get; set; }

        [Required(ErrorMessage = "House Number/Name is required")]
        [Display(Name = "House Number/Name")]
        [MaxLength(50, ErrorMessage = "House Number/Name cannot be longer than 50 characters")]
        public string HouseNumberOrName { get; set; }

        [Required(ErrorMessage = "Street is required")]
        [Display(Name = "Street")]
        [MaxLength(50, ErrorMessage = "Street cannot be longer than 50 characters")]
        public string Street { get; set; }

        [Required(ErrorMessage = "Town/City is required")]
        [Display(Name = "Town/City")]
        [MaxLength(50, ErrorMessage = "Town/City cannot be longer than 50 characters")]
        public string City { get; set; }

        [Required(ErrorMessage = "Postal Code is required")]
        [Display(Name = "Postal Code")]
        [MaxLength(10, ErrorMessage = "Postal Code cannot be longer than 10 characters")]
        public string PostCode { get; set; }
    }
}