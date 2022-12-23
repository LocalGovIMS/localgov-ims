using System;

namespace BusinessLogic.Classes
{
    public class PaymentDetails
    {
        public string Fund { get; set; }
        public decimal Amount { get; set; }
        public string Source { get; set; }
        public string SuccessUrl { get; set; }
        public string CancelUrl { get; set; }
        public string FailUrl { get; set; }
        public DateTime CreatedAt { get; set; }
        public string AccountReference { get; set; }
        public string MopCode { get; set; }
        public string Narrative { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressLine4 { get; set; }
        public string Postcode { get; set; }
        public string VatCode { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public string AppReference { get; set; }
        public string BankAccountNo { get; set; }
        public string ChequeNumber { get; set; }
        public string SortCode { get; set; }
        public string ChequeName { get; set; }
        public string PayeeName { get; set; }
        public string CallRecordingSource { get; set; }
        public string CallRecordingUserName { get; set; }
    }
}