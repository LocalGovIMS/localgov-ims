using System.Linq;

namespace Admin.Models.Transaction
{
    public class DetailsViewModel
    {
        public BusinessLogic.Models.Transaction Transaction { get; set; }

        public RefundViewModel Refund { get; set; }

        public bool IsRefund
        {
            get { return Transaction.TransactionLines.Any(x => x.RefundReference != null); }
        }
    }
}