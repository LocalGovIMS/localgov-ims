using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IFundGroupFundRepository : IRepository<FundGroupFund>
    {
        List<FundGroupFund> GetFundGroupFundsByFundGroupId(int id);
        List<FundGroupFund> GetFundGroupFunds(int id);
        List<FundGroupFund> GetAllExtended();
        void UpdateFundGroupFunds(List<FundGroupFund> items);
    }
}