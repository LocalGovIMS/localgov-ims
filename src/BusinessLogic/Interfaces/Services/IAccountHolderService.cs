using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.AccountHolder;
using BusinessLogic.Models.Shared;
using BusinessLogic.Services;

namespace BusinessLogic.Interfaces.Services
{
    public interface IAccountHolderService
    {
        IResult Create(AccountHolder accountHolder);
        IResult Create(CreateAccountHolderArgs args);
        IResult Update(AccountHolder accountHolder);
        AccountHolder GetByAccountReference(string accountReference);
        AccountHolder GetByAccountReference(string accountReference, string fundCode);
        SearchResult<AccountHolder> Search(SearchCriteria searchCriteria);
    }
}