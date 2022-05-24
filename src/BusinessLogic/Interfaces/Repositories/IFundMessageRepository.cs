using BusinessLogic.Entities;
using BusinessLogic.Models.FundMessage;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IFundMessageRepository : IRepository<FundMessage>
    {
        void Update(FundMessage entity);
        new IEnumerable<FundMessage> GetAll();

        FundMessage GetById(int id);
        
        IEnumerable<FundMessage> Search(SearchCriteria criteria, out int resultCount);
    }
}