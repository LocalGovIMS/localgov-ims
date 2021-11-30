namespace BusinessLogic.Classes
{
    public class PaymentResult
    {
        public string AuthResult { get; set; }
        public string PspReference { get; set; }
        public string MerchantReference { get; set; }
        public string MerchantSig { get; set; }
        public string PaymentMethod { get; set; }
    }
}