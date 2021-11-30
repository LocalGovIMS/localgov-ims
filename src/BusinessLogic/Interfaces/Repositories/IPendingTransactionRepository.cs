using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IPendingTransactionRepository : IRepository<PendingTransaction>
    {
        IEnumerable<PendingTransaction> GetByInternalReference(string reference);
        IEnumerable<PendingTransaction> GetByRefundReference(string reference);
        IEnumerable<PendingTransaction> GetPendingRefunds(string transactionReference);
        IEnumerable<PendingTransaction> GetFailedRefunds(string transactionReference);
        void Update(List<PendingTransaction> items, int eReturnId);
    }
}