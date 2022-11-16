using BusinessLogic.Classes;
using System.Collections.Generic;

namespace Api.Controllers.PendingTransactions
{
    public class PaymentModel
    {
        public Address Address { get; set; }

        public List<BasketItem> Basket { get; set; }

        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
        public string FailUrl { get; set; }
        public string MopCode { get; set; }
        public string ExternalReference { get; set; }
        public string CallRecordingSource { get; set; }
        public string CallRecordingUserName { get; set; }

        public List<PaymentDetails> GetPaymentDetails()
        {
            var paymentDetailsList = new List<PaymentDetails>();

            foreach (var item in Basket)
            {
                paymentDetailsList.Add(new PaymentDetails()
                {
                    AccountReference = item.AccountReference,
                    Fund = item.Fund,
                    Amount = item.Amount,
                    Narrative = item.Narrative,
                    VatCode = item.VatCode,
                    CancelUrl = CancelUrl,
                    FailUrl = FailUrl,
                    SuccessUrl = SuccessUrl,
                    PayeeName = Address.PayeeName ?? string.Empty,
                    AddressLine1 = Address.AddressLine1 ?? string.Empty,
                    AddressLine2 = Address.AddressLine2 ?? string.Empty,
                    AddressLine3 = Address.AddressLine3 ?? string.Empty,
                    AddressLine4 = Address.AddressLine4 ?? string.Empty,
                    Postcode = Address.Postcode ?? string.Empty,
                    AppReference = ExternalReference,
                    MopCode = MopCode,
                    CallRecordingSource = CallRecordingSource,
                    CallRecordingUserName = CallRecordingUserName
                });
            }

            return paymentDetailsList;
        }
    }

    public class Address
    {
        public string PayeeName { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string Postcode { get; set; }
    }

    public class BasketItem
    {
        public string Fund { get; set; }
        public decimal Amount { get; set; }
        public string AccountReference { get; set; }
        public string Narrative { get; set; }
        public string VatCode { get; set; }
    }
}