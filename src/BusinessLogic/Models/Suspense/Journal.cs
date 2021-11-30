namespace BusinessLogic.Models.Suspense
{
    public class Journal
    {
        public string FundCode { get; set; }
        public string VatCode { get; set; }
        public string MopCode { get; set; }
        public decimal Amount { get; set; }
        public string AccountReference { get; set; }
        public string Narrative { get; set; }
    }
}
