using System;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Models.Payments
{
    [Serializable]
    public class BasketItem
    {
        [Display(Name = "Fund")]
        public string FundCode { get; set; }

        [Display(Name = "Fund")]
        public string FundName { get; set; }

        [Display(Name = "Account reference")]
        public string AccountReference { get; set; }

        public string Narrative { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Amount { get; set; }

        [Display(Name = "VAT code")]
        public string VatCode { get; set; }

        [Display(Name = "MOP code")]
        public string MopCode { get; set; }

        [Display(Name = "MOP name")]
        public string MopName { get; set; }
    }
}