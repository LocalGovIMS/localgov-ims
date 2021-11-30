namespace BusinessLogic.Clients.PaymentIntegrationClient
{
    public class RefundResponse
    {
        public string PspReference { get; set; }
        public decimal? Amount { get; set; }
        public string Message { get; set; }
        public bool Success { get; set; }
    }
}
