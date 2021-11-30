using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Persistence;

namespace DataAccess.Repositories
{
    public class EReturnTypeRepository : Repository<EReturnType>, IEReturnTypeRepository
    {
        public EReturnTypeRepository(IncomeDbContext context) : base(context)
        {
        }
    }
}