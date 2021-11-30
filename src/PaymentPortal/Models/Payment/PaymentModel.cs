using PaymentPortal.Models.Payment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace PaymentPortal.Models
{
    [Serializable]
    public class PaymentModel
    {
        public IEnumerable<SelectListItem> PaymentTypes { get; set; }

        [DisplayName("Payment type")]
        [Required(ErrorMessage = "A payment type must be selected")]
        public string PaymentType { get; set; }

        public string PaymentTypeDescription { get; set; }

        private string _paymentReference;

        [DisplayName("Payment reference")]
        [Required(ErrorMessage = "A payment reference must be entered")]
        public string PaymentReference
        {
            get { return _paymentReference; }
            set { _paymentReference = value?.Trim(); }
        }

        [DisplayName("Payment amount (£)")]
        [Range(0, 9999999999, ErrorMessage = "Please enter a valid payment amount")]
        [DisplayFormat(DataFormatString = "0.00", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage = "A payment amount is required")]
        public decimal Amount { get; set; }

        public List<BasketRow> BasketItems { get; set; }

        public List<string> PaymentDescriptions { get; set; }
        
        //restrict upto 10 items for shopping cart
        public string Payment_1 { get; set; }
        public string Payment_2 { get; set; }
        public string Payment_3 { get; set; }
        public string Payment_4 { get; set; }
        public string Payment_5 { get; set; }
        public string Payment_6 { get; set; }
        public string Payment_7 { get; set; }
        public string Payment_8 { get; set; }
        public string Payment_9 { get; set; }
        public string Payment_10 { get; set; }

        public PaymentAddress PaymentAddressDetails { get; set; }

        [DisplayName("Basket Total : ")]
        [DataType(DataType.Currency)]
        public decimal BasketTotal => BasketItems?.Sum(x => x.Amount) ?? 0;

        public PaymentModel()
        {
            BasketItems = new List<BasketRow>();
            PaymentDescriptions = new List<string>();
        }
                
        public void AddBasketItem()
        {
            BasketItems.Add(new BasketRow
            {
                Code = PaymentType,
                Reference = PaymentReference,
                Description = PaymentTypeDescription,
                Amount = Amount
            });
        }

        public void RemoveBasketItem(string reference)
        {
            BasketItems.RemoveAll(x => x.Reference == reference);

            DeletePaymentString();
        }

        public void DeletePaymentString()
        {
            Payment_1 = null;
            Payment_2 = null;
            Payment_3 = null;
            Payment_4 = null;
            Payment_5 = null;
            Payment_6 = null;
            Payment_7 = null;
            Payment_8 = null;
            Payment_9 = null;
            Payment_10 = null;
        }

        public void ClearPaymentDetails()
        {
            PaymentReference = "";
            Amount = 0;
            PaymentType = null;
        }
    }
}