using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.TransactionImportTypeImportProcessingRule;
using BusinessLogic.Models.Shared;

namespace BusinessLogic.Interfaces.Services
{
    public interface ITransactionImportTypeImportProcessingRuleService
    {
        IResult Create(TransactionImportTypeImportProcessingRule item);
        TransactionImportTypeImportProcessingRule Get(int id);
        SearchResult<TransactionImportTypeImportProcessingRule> Search(SearchCriteria criteria);
        IResult Update(TransactionImportTypeImportProcessingRule item);
        IResult Delete(int id);
    }
}