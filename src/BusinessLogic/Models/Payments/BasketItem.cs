using System;

namespace BusinessLogic.Models.Payments
{
    [Serializable]
    public class BasketItem
    {
        public string FundCode { get; set; }
        public string FundName { get; set; }
        public string AccountReference { get; set; }
        public string Narrative { get; set; }
        public decimal Amount { get; set; }
        public string VatCode { get; set; }
        public string MopCode { get; set; }
        public string MopName { get; set; }
    }
}