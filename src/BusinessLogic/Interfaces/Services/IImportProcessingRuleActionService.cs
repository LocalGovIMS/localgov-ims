using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.ImportProcessingRuleAction;
using BusinessLogic.Models.Shared;

namespace BusinessLogic.Interfaces.Services
{
    public interface IImportProcessingRuleActionService
    {
        IResult Create(ImportProcessingRuleAction item);
        ImportProcessingRuleAction Get(int id);
        SearchResult<ImportProcessingRuleAction> Search(SearchCriteria criteria);
        IResult Update(ImportProcessingRuleAction item);
        IResult Delete(int id);
    }
}