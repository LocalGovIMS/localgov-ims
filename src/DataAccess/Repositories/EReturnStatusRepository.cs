using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Persistence;

namespace DataAccess.Repositories
{
    public class EReturnStatusRepository : Repository<EReturnStatus>, IEReturnStatusRepository
    {
        public EReturnStatusRepository(IncomeDbContext context) : base(context)
        {
        }
    }
}