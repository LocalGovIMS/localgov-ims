using BusinessLogic.Entities;
using BusinessLogic.Models.AccountReferenceValidator;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IAccountReferenceValidatorRepository : IRepository<AccountReferenceValidator>
    {
        AccountReferenceValidator Get(int id);
        AccountReferenceValidator GetByFundCode(string fundCode);
        IEnumerable<AccountReferenceValidator> Search(SearchCriteria criteria, out int resultCount);
        void Update(AccountReferenceValidator entity);
    }
}