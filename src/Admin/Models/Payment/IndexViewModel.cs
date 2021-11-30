using Admin.Models.Shared;
using BusinessLogic.Models.Payments;
using System;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.Payment
{
    [Serializable]
    public class IndexViewModel
    {
        public SelectList Funds { get; set; }

        public SelectList VatCodes { get; set; }

        public SelectList MopCodes { get; set; }

        [Display(Name = "Fund type")]
        [Required(ErrorMessage = "Fund type is required")]
        public string FundCode { get; set; }

        [Display(Name = "MOP code")]
        [Required(ErrorMessage = "MOP code is required")]
        public string MopCode { get; set; }

        [Display(Name = "Reference")]
        [Required(ErrorMessage = "Reference is required")]
        public string AccountReference { get; set; }

        [Display(Name = "Narrative")]
        public string Narrative { get; set; }

        [Display(Name = "Amount")]
        [Required(ErrorMessage = "Amount is required")]
        //[Range(0.01, int.MaxValue, ErrorMessage = "The amount specified is not valid")]
        public decimal Amount { get; set; }

        public Basket Basket { get; set; }

        public Message Message { get; set; }

        public Address Address { get; set; }

        public Cheques Cheques { get; set; }

        public bool AddressReviewed { get; set; }

        public string SearchEnabledFundCodes { get; set; }

        [Display(Name = "VAT code")]
        [Required(ErrorMessage = "VAT code is required")]
        public string VatCode { get; set; }
    }
}