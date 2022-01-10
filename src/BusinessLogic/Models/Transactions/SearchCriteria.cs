using System;

namespace BusinessLogic.Models.Transactions
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public string ReceiptNumber { get; set; }
        public string[] FundCodes { get; set; }
        public string AccountReference { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string AppReference { get; set; }
        public string[] MopCodes { get; set; }
        public int? UserId { get; set; }
        public string Narrative { get; set; }
        public string InternalReference { get; set; }
        public bool WildSearchAccountReference { get; set; }
        public string BatchReference { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}