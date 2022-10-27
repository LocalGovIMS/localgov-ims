using Admin.Models.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace Admin.Models.Payment
{
    [Serializable]
    public class Address
    {
        private const string IsNotJustNumericExpression = @"(?!^[\d\W]+$)^.+$";

        [Required(ErrorMessage = "Payee Name is required")]
        [Display(Name = "Payee Name")]
        [MaxLength(50, ErrorMessage = "Payee Name cannot be longer than 50 characters")]
        [RegularExpression(IsNotJustNumericExpression, ErrorMessage = "Payee name should contain letters and can include numbers")]
        public string PayeeName { get; set; }

        [Required(ErrorMessage = "Address line 1 is required")]
        [Display(Name = "Address line 1")]
        [MaxLength(50, ErrorMessage = "Address line 1 cannot be longer than 50 characters")]
        [RegularExpression(IsNotJustNumericExpression, ErrorMessage = "Address line 1 should contain letters and can include numbers")]
        public string AddressLine1 { get; set; }

        [Display(Name = "Address line 2")]
        [MaxLength(50, ErrorMessage = "Address line 2 cannot be longer than 50 characters")]
        [RegularExpression(IsNotJustNumericExpression, ErrorMessage = "Address line 2 should contain letters and can include numbers")]
        public string AddressLine2 { get; set; }

        [Required(ErrorMessage = "Town or City is required")]
        [Display(Name = "Town or City")]
        [MaxLength(50, ErrorMessage = "Town or City cannot be longer than 50 characters")]
        [RegularExpression(IsNotJustNumericExpression, ErrorMessage = "Town or city should contain letters and can include numbers")]
        public string AddressLine3 { get; set; }

        [Display(Name = "County")]
        [MaxLength(50, ErrorMessage = "County cannot be longer than 50 characters")]
        [RegularExpression(IsNotJustNumericExpression, ErrorMessage = "County should contain letters and can include numbers")]
        public string AddressLine4 { get; set; }

        [Required(ErrorMessage = "Postcode is required")]
        [Display(Name = "Postcode")]
        [MaxLength(10, ErrorMessage = "Postcode cannot be longer than 10 characters")]
        public string PostCode { get; set; }

        public Message Message { get; set; }

        public bool? CallRecordingMsgShown { get; set; }
        public string CallRecordingMsg { get; set; }
    }
}