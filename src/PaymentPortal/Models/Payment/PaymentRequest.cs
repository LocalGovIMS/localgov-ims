using BusinessLogic.Classes;
using System;
using System.Collections.Generic;

namespace PaymentPortal.Models.Payment
{
    public class PaymentRequest
    {
        public string CallingApplicationId { get; set; }
        public string CallingApplicationTransactionReference { get; set; }
        public string MopCode { get; set; }
        public string ReturnUrl { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HouseNameOrNumber { get; set; }
        public string Street { get; set; }
        public string Locality { get; set; }
        public string Town { get; set; }
        public string PostCode { get; set; }
        public string AccountReference { get; set; }
        public DateTime? SessionExpiry { get; set; }
        public string CallingRecordingSource { get; set; }
        public string CallingRecordingUserName { get; set; }
        public List<PaymentRequestRow> PaymentRow { get; set; }

        public List<PaymentDetails> GetPaymentDetailsList()
        {
            var paymentDetails = new List<PaymentDetails>();
            foreach (var row in PaymentRow)
            {
                paymentDetails.Add(new PaymentDetails
                {
                    AccountReference = row.AccountReference,
                    Narrative = row.Narrative,
                    VatCode = row.VatCode,
                    Amount = decimal.Parse(row.Amount),
                    Fund = row.FundCode,
                    AppReference = CallingApplicationTransactionReference,
                    CancelUrl = ReturnUrl,
                    CreatedAt = DateTime.Now,
                    ExpiryDate = SessionExpiry,
                    FailUrl = ReturnUrl,
                    SuccessUrl = ReturnUrl,
                    IsLegacy = true,
                    PayeeName = FirstName + " " + LastName,
                    AddressLine1 = HouseNameOrNumber + " " + Street,
                    AddressLine3 = Town,
                    MopCode = MopCode,
                    Postcode = PostCode,
                    Source = CallingApplicationId,
                    CallRecordingSource = CallingRecordingSource,
                    CallRecordingUserName = CallingRecordingUserName
                });
            }
            return paymentDetails;
        }
    }

    public class PaymentRequestRow
    {
        public string AccountReference { get; set; }
        public string Narrative { get; set; }
        public string VatCode { get; set; }
        public string Amount { get; set; }
        public string FundCode { get; set; }
    }
}