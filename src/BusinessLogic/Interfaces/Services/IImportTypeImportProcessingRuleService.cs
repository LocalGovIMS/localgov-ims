using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.ImportTypeImportProcessingRule;
using BusinessLogic.Models.Shared;

namespace BusinessLogic.Interfaces.Services
{
    public interface IImportTypeImportProcessingRuleService
    {
        IResult Create(ImportTypeImportProcessingRule item);
        ImportTypeImportProcessingRule Get(int id);
        SearchResult<ImportTypeImportProcessingRule> Search(SearchCriteria criteria);
        IResult Update(ImportTypeImportProcessingRule item);
        IResult Delete(int id);
    }
}