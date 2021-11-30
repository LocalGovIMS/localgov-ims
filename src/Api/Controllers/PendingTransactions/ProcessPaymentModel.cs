namespace Api.Controllers.PendingTransactions
{
    public class ProcessPaymentModel
    {
        public string AuthResult { get; set; }
        public string PspReference { get; set; }
        public string MerchantReference { get; set; }
        public string PaymentMethod { get; set; }
    }
}