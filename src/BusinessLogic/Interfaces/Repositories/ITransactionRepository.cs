using BusinessLogic.Entities;
using BusinessLogic.Models.Transactions;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface ITransactionRepository : IRepository<ProcessedTransaction>
    {
        IEnumerable<ProcessedTransaction> GetByInternalReference(string reference);
        List<ProcessedTransaction> Search(SearchCriteria criteria);
        List<ProcessedTransaction> Search(SearchCriteria criteria, out int resultCount);
        IEnumerable<ProcessedTransaction> GetProcessedRefunds(string reference);
        IEnumerable<ProcessedTransaction> GetTransfers(string transferGuid);
        IEnumerable<ProcessedTransaction> GetJournalsForTransactions(IEnumerable<ProcessedTransaction> transactions);
        IEnumerable<ProcessedTransaction> GetByPspReference(string pspReference);
        ProcessedTransaction GetByTransactionReference(string reference);
        ProcessedTransaction GetByAppReference(string appReference);
    }
}