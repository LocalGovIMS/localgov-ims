using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.ImportProcessingRule;
using BusinessLogic.Models.Shared;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IImportProcessingRuleService
    {
        IResult Create(ImportProcessingRule item);
        ImportProcessingRule Get(int id);
        List<ImportProcessingRule> GetAll(bool includeDisabled);
        SearchResult<ImportProcessingRule> Search(SearchCriteria criteria);
        IResult Update(ImportProcessingRule item);
    }
}