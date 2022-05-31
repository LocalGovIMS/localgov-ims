using BusinessLogic.Models.Transactions;

namespace Api.Controllers.ProcessedTransactions
{
    public class UpdateCardDetailsModel
    {
        public string MerchantReference { get; set; }
        public string CardPrefix { get; set; }
        public string CardSuffix { get; set; }

        public UpdateCardDetailsArgs ToUpdateCardDetailsArgs()
        {
            return new UpdateCardDetailsArgs
            {
                MerchantReference = this.MerchantReference,
                CardPrefix = this.CardPrefix,
                CardSuffix = this.CardSuffix
            };
        }
    }
}