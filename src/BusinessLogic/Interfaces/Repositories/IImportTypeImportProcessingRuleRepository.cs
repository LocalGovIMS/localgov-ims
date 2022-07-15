using BusinessLogic.Entities;
using BusinessLogic.Models.ImportTypeImportProcessingRule;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IImportTypeImportProcessingRuleRepository : IRepository<ImportTypeImportProcessingRule>
    {
        ImportTypeImportProcessingRule Get(int id);
        IEnumerable<ImportTypeImportProcessingRule> Search(SearchCriteria criteria, out int resultCount);
        void Update(ImportTypeImportProcessingRule entity);
    }
}