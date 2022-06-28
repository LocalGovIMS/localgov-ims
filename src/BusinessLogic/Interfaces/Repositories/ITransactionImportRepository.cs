using BusinessLogic.Entities;
using BusinessLogic.Models.TransactionImport;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface ITransactionImportRepository : IRepository<TransactionImport>
    {
        TransactionImport Get(int id);
        List<TransactionImport> Search(SearchCriteria criteria, out int resultCount);
    }
}