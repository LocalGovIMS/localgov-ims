using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.AccountReferenceValidator;
using BusinessLogic.Models.Shared;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IAccountReferenceValidatorService
    {
        IResult Create(AccountReferenceValidator item);
        SearchResult<AccountReferenceValidator> Search(SearchCriteria criteria);
        List<AccountReferenceValidator> GetAll();
        AccountReferenceValidator Get(int id);
        AccountReferenceValidator GetByFundCode(string fundCode);
        IResult Update(AccountReferenceValidator item);
        IResult Delete(int id);
    }
}