namespace BusinessLogic.Models.Transactions
{
    public class AuthorisePendingTransactionByInternalReferenceArgs
    {
        public string InternalReference { get; set; }
        public string PspReference { get; set; }
        public string CardPrefix { get; set; }
        public string CardSuffix { get; set; }
    }
}
