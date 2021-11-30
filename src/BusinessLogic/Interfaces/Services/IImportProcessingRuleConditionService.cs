using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.ImportProcessingRuleCondition;
using BusinessLogic.Models.Shared;

namespace BusinessLogic.Interfaces.Services
{
    public interface IImportProcessingRuleConditionService
    {
        IResult Create(ImportProcessingRuleCondition item);
        ImportProcessingRuleCondition Get(int id);
        SearchResult<ImportProcessingRuleCondition> Search(SearchCriteria criteria);
        IResult Update(ImportProcessingRuleCondition item);
        IResult Delete(int id);
    }
}