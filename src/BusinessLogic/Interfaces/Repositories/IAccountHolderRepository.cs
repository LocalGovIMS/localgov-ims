using BusinessLogic.Entities;
using BusinessLogic.Models.AccountHolder;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IAccountHolderRepository : IRepository<AccountHolder>
    {
        AccountHolder GetByAccountReference(string accountReference);
        AccountHolder GetByAccountReference(string accountReference, string fundCode);
        List<AccountHolder> Search(SearchCriteria criteria);
        List<AccountHolder> Search(SearchCriteria criteria, out int resultCount);
    }
}