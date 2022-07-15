using BusinessLogic.Entities;
using BusinessLogic.Models.ImportProcessingRule;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IImportProcessingRuleRepository : IRepository<ImportProcessingRule>
    {
        IEnumerable<ImportProcessingRule> Search(SearchCriteria criteria, out int resultCount);
        ImportProcessingRule Get(int id);
        IEnumerable<ImportProcessingRule> GetAll(bool includeDisabled);
        IEnumerable<ImportProcessingRule> GetByImportType(int importTypeId);
        void Update(ImportProcessingRule entity);
    }
}