using BusinessLogic.Classes;

namespace Api.Controllers.PendingTransactions
{
    public class PaymentResponseModel
    {
        public string PaymentUrl { get; set; }
        public string Reference { get; set; } // PendingTransaction.InternalReference

        public PaymentResponseModel(PaymentResponse response)
        {
            PaymentUrl = response.ResponseUrl;
            Reference = response.ResponseId;
        }
    }
}