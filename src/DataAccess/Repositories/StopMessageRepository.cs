using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Persistence;

namespace DataAccess.Repositories
{
    public class StopMessageRepository : Repository<StopMessage>, IStopMessageRepository
    {
        public StopMessageRepository(IncomeDbContext context) : base(context)
        {
        }
    }
}