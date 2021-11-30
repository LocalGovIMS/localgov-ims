using BusinessLogic.Entities;
using BusinessLogic.Models.ImportProcessingRuleAction;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IImportProcessingRuleActionRepository : IRepository<ImportProcessingRuleAction>
    {
        IEnumerable<ImportProcessingRuleAction> Search(SearchCriteria criteria, out int resultCount);
        ImportProcessingRuleAction Get(int id);
        void Update(ImportProcessingRuleAction entity);
    }
}