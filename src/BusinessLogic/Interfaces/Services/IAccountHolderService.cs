using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.AccountHolder;
using BusinessLogic.Models.Shared;

namespace BusinessLogic.Interfaces.Services
{
    public interface IAccountHolderService
    {
        AccountValidation GetAccountValidation(string validationReference);
        IResult Create(AccountHolder accountHolder);
        IResult Update(AccountHolder accountHolder);
        AccountHolder GetByAccountReference(string accountReference);
        AccountHolder GetByAccountReference(string accountReference, string fundCode);
        SearchResult<AccountHolder> Search(SearchCriteria searchCriteria);
    }
}