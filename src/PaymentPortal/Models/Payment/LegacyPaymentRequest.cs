using BusinessLogic.Classes;
using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable InconsistentNaming

namespace PaymentPortal.Models.Payment
{
    public class LegacyPaymentRequest
    {
        public string CallingApplicationID { get; set; }
        public string CallingApplicationTransactionReference { get; set; }
        public string MopCode { get; set; }
        public string ReturnUrl { get; set; }
        public string Payment_1 { get; set; }
        public string PaymentData_1 { get; set; }
        public string Payment_2 { get; set; }
        public string Payment_3 { get; set; }
        public string Payment_4 { get; set; }
        public string Payment_5 { get; set; }
        public string Payment_6 { get; set; }
        public string Payment_7 { get; set; }
        public string Payment_8 { get; set; }
        public string Payment_9 { get; set; }
        public string PaymentTotal { get; set; }
        public string FundCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HouseNameOrNumber { get; set; }
        public string Street { get; set; }
        public string Locality { get; set; }
        public string Town { get; set; }
        public string PostCode { get; set; }
        public string AccountReference { get; set; }
        public string VatCode { get; set; }
        public string Amount { get; set; }
        public string Narrative { get; set; }
        public DateTime? SessionExpiry { get; set; }
        public string Hash { get; set; }
        public string CallRecordingSource { get; set; }
        public string CallRecordingUserName { get; set; }
        public List<PaymentDetails> PaymentDetails { get; private set; }

        public LegacyPaymentRequest()
        {
            PaymentDetails = new List<PaymentDetails>();
        }

        public void Populate()
        {
            if (Payment_1 == null) return;

            if (SessionExpiry == null)
            {
                SessionExpiry = DateTime.Now.AddMinutes(10);
            }

            if (Hash != null)
            {
                ReturnUrl += (ReturnUrl.Contains("?") ? "&" : "?") + "hash=" + Hash;
            }

            AddPaymentRow(Payment_1);
            AddPaymentRow(Payment_2);
            AddPaymentRow(Payment_3);
            AddPaymentRow(Payment_4);
            AddPaymentRow(Payment_5);
            AddPaymentRow(Payment_6);
            AddPaymentRow(Payment_7);
            AddPaymentRow(Payment_8);
            AddPaymentRow(Payment_9);
        }

        private void AddPaymentRow(string paymentString)
        {
            if (paymentString == null) return;

            var paymentFragments = paymentString.Split('|');

            var paymentDetails = new PaymentDetails()
            {
                AccountReference = paymentFragments[0],
                Fund = paymentFragments[1],
                Amount = decimal.Parse(paymentFragments[2]),
                VatCode = paymentFragments[3]
            };

            if (paymentFragments[4].Contains(' '))
            {
                paymentDetails.PayeeName = string.Format("{0}, {1}", paymentFragments[4].Split(' ')[0],
                    paymentFragments[4].Split(' ')[1]);
            }
            paymentDetails.Narrative = paymentFragments[4];
            if (paymentFragments.Length < 6)
            {
                paymentDetails.AddressLine1 = "";
                paymentDetails.AddressLine2 = "";
                paymentDetails.AddressLine3 = "";
                paymentDetails.AddressLine4 = "";
                paymentDetails.Postcode = "";
            }
            else
            {
                if (string.IsNullOrWhiteSpace(paymentDetails.AddressLine1)
                    && string.IsNullOrWhiteSpace(paymentDetails.AddressLine2)
                    && string.IsNullOrWhiteSpace(paymentDetails.AddressLine3)
                    && string.IsNullOrWhiteSpace(paymentDetails.AddressLine4)
                    && string.IsNullOrWhiteSpace(paymentDetails.Postcode))
                {
                    paymentDetails.AddressLine1 = string.IsNullOrWhiteSpace(paymentFragments[7]) ? paymentFragments[6] : paymentFragments[7];
                    paymentDetails.AddressLine2 = paymentFragments[8];
                    paymentDetails.AddressLine3 = paymentFragments[10];
                    paymentDetails.Postcode = paymentFragments[12];

                    HouseNameOrNumber = string.IsNullOrWhiteSpace(paymentFragments[7]) ? paymentFragments[6] : paymentFragments[7];
                    Street = paymentFragments[8];
                    Town = paymentFragments[10];
                    PostCode = paymentFragments[12];
                }
            }

            paymentDetails.AppReference = CallingApplicationTransactionReference;
            paymentDetails.SuccessUrl = ReturnUrl;
            paymentDetails.CancelUrl = ReturnUrl;
            paymentDetails.FailUrl = ReturnUrl;
            paymentDetails.Source = CallingApplicationID;
            paymentDetails.MopCode = MopCode;
            paymentDetails.ExpiryDate = SessionExpiry;
            paymentDetails.CallRecordingSource = CallRecordingSource;
            paymentDetails.CallRecordingUserName = CallRecordingUserName;
            PaymentDetails.Add(paymentDetails);
        }
    }
}