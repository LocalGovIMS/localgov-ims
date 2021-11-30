namespace BusinessLogic.Models.Suspense
{
    public class CreditNote
    {
        public string AccountReference { get; set; }
        public decimal Amount { get; set; }
        public string FundCode { get; set; }
        public string VatCode { get; set; }
    }
}
