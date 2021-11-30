namespace BusinessLogic.Classes
{
    public class RefundRequest
    {
        public string TransactionReference { get; set; }
        public decimal RefundAmount { get; set; }

        public RefundRequest() { }

        public RefundRequest(string transactionReference, decimal refundAmount)
        {
            TransactionReference = transactionReference;
            RefundAmount = refundAmount;
        }
    }
}