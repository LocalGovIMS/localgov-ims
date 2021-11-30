using BusinessLogic.Models.Transactions;
using System;

namespace Api.Controllers.ProcessedTransactions
{
    public class SearchCriteriaModel
    {
        public string ReceiptNumber { get; set; }
        public string[] FundCodes { get; set; }
        public string AccountReference { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string AppReference { get; set; }
        public string[] MopCodes { get; set; }
        public string Narrative { get; set; }
        public string InternalReference { get; set; }
        public string PspReference { get; set; }

        public SearchCriteria ToSearchCriteria()
        {
            return new SearchCriteria
            {
                ReceiptNumber = ReceiptNumber,
                FundCodes = FundCodes,
                AccountReference = AccountReference,
                Amount = Amount, // TODO: We need an operator here too, e.g. <=, or >
                StartDate = StartDate,
                EndDate = EndDate,
                AppReference = AppReference,
                MopCodes = MopCodes,
                UserId = null,
                Narrative = Narrative,
                InternalReference = InternalReference
            };
        }
    }
}