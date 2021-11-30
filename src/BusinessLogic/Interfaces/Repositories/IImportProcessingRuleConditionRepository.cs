using BusinessLogic.Entities;
using BusinessLogic.Models.ImportProcessingRuleCondition;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IImportProcessingRuleConditionRepository : IRepository<ImportProcessingRuleCondition>
    {
        IEnumerable<ImportProcessingRuleCondition> Search(SearchCriteria criteria, out int resultCount);
        ImportProcessingRuleCondition Get(int id);
        void Update(ImportProcessingRuleCondition entity);
    }
}