using System.Linq;

namespace BusinessLogic.Models.Suspense
{
    public class SuspenseWrapper
    {
        public Entities.Suspense Item { get; private set; }

        public decimal AmountRemaining
        {
            get
            {
                return Item.Amount - AmountAllocated;
            }
        }

        public decimal AmountAllocated
        {
            get
            {
                return Item.SuspenseProcessedTransactions.Sum(x => x.Amount);
            }
        }

        public Entities.SuspenseNote LatestNote
        {
            get
            {
                return Item.SuspenseNotes?
                    .OrderByDescending(x => x.CreatedAt)
                    .FirstOrDefault();
            }
        }

        public SuspenseWrapper(Entities.Suspense item)
        {
            Item = item;
        }
    }
}
