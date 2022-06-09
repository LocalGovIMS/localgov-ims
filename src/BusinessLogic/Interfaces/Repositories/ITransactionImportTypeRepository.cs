using BusinessLogic.Entities;
using BusinessLogic.Models.TransactionImportType;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface ITransactionImportTypeRepository : IRepository<TransactionImportType>
    {
        IEnumerable<TransactionImportType> Search(SearchCriteria criteria, out int resultCount);
        TransactionImportType Get(int id);
        void Update(TransactionImportType entity);
    }
}