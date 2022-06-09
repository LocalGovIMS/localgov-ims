using BusinessLogic.Entities;
using BusinessLogic.Models.TransactionImportTypeImportProcessingRule;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface ITransactionImportTypeImportProcessingRuleRepository : IRepository<TransactionImportTypeImportProcessingRule>
    {
        TransactionImportTypeImportProcessingRule Get(int id);
        IEnumerable<TransactionImportTypeImportProcessingRule> Search(SearchCriteria criteria, out int resultCount);
        void Update(TransactionImportTypeImportProcessingRule entity);
    }
}