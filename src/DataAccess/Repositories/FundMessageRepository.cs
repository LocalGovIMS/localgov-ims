using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Persistence;

namespace DataAccess.Repositories
{
    public class FundMessageRepository : Repository<FundMessage>, IFundMessageRepository
    {
        public FundMessageRepository(IncomeDbContext context) : base(context)
        {
        }
    }
}