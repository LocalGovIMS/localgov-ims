using System;

namespace BusinessLogic.Models
{
    [Serializable]
    public class TransferItem
    {
        public Guid Id { get; set; }
        public string FundCode { get; set; }
        public string VatCode { get; set; }
        public string MopCode { get; set; }
        public string FundName { get; set; }
        public string AccountReference { get; set; }
        public decimal Amount { get; set; }
        public string Narrative { get; set; }
        public string BatchReference { get; set; }
    }
}