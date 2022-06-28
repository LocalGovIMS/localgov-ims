using BusinessLogic.Entities;
using BusinessLogic.Models.Shared;
using BusinessLogic.Models.TransactionImport;

namespace BusinessLogic.Interfaces.Services
{
    public interface ITransactionImportService
    {
        SearchResult<TransactionImport> Search(SearchCriteria criteria);
        TransactionImport Get(int id);
    }
}