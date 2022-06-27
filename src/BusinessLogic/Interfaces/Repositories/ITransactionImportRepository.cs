using BusinessLogic.Entities;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface ITransactionImportRepository : IRepository<TransactionImport>
    {
        TransactionImport Get(int id);
    }
}