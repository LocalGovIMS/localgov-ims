using System;

namespace BusinessLogic.Clients.PaymentIntegrationClient
{
    public class RefundRequest
    {
        public string Reference { get; set; }
        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
