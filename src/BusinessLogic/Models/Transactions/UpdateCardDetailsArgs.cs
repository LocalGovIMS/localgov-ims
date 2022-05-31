namespace BusinessLogic.Models.Transactions
{
    public class UpdateCardDetailsArgs
    {
        public string MerchantReference { get; set; }
        public string CardPrefix { get; set; }
        public string CardSuffix { get; set; }
    }
}
