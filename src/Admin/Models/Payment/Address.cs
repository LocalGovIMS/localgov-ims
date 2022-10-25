using Admin.Models.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.Payment
{
    [Serializable]
    public class Address
    {
        [Required(ErrorMessage = "Payee Name is required")]
        [Display(Name = "Payee Name")]
        [MaxLength(50, ErrorMessage = "Payee Name cannot be longer than 50 characters")]
        public string PayeeName { get; set; }

        [Required(ErrorMessage = "Address line 1 is required")]
        [Display(Name = "Address line 1")]
        [MaxLength(50, ErrorMessage = "Address line 1 cannot be longer than 50 characters")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address line 2")]
        [MaxLength(50, ErrorMessage = "Address line 2 cannot be longer than 50 characters")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Town or City is required")]
        [Display(Name = "Town or City")]
        [MaxLength(50, ErrorMessage = "Town or City cannot be longer than 50 characters")]
        public string AddressLine3 { get; set; }

        [Display(Name = "County")]
        [MaxLength(50, ErrorMessage = "County cannot be longer than 50 characters")]
        public string AddressLine4 { get; set; }

        [Required(ErrorMessage = "Post Code is required")]
        [Display(Name = "Postal Code")]
        [MaxLength(10, ErrorMessage = "Postal Code cannot be longer than 10 characters")]
        public string PostCode { get; set; }

        public Message Message { get; set; }

        public bool? CallRecordingMsgShown { get; set; }
        public string CallRecordingMsg { get; set; }
    }
}