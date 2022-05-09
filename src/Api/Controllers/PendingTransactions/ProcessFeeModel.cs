using BusinessLogic.Classes;

namespace Api.Controllers.PendingTransactions
{
    public class ProcessFeeModel
    {
        public string MerchantReference { get; set; }
        public string PspReference { get; set; }
        public decimal Fee { get; set; }

        public PaymentResult ToPaymentResult()
        {
            return new PaymentResult
            {
                MerchantReference = this.MerchantReference,
                PspReference = this.PspReference,
                Fee = this.Fee
            };
        }
    }
}