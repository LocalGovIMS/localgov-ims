using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Persistence;

namespace DataAccess.Repositories
{
    public class TransactionNotificationRepository : Repository<TransactionNotification>, ITransactionNotificationRepository
    {
        public TransactionNotificationRepository(IncomeDbContext context) : base(context)
        {
        }
    }
}