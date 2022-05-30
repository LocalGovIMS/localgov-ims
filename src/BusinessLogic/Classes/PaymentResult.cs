namespace BusinessLogic.Classes
{
    public class PaymentResult
    {
        public string AuthResult { get; set; }
        public string PspReference { get; set; }
        public string MerchantReference { get; set; }
        public string MerchantSig { get; set; }
        public string PaymentMethod { get; set; }
        public decimal AmountPaid { get; set; }
        public decimal Fee { get; set; }
        public string CardPrefix { get; set; }
        public string CardSuffix { get; set; }
    }
}