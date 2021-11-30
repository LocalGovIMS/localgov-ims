using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Persistence;

namespace DataAccess.Repositories
{
    public class AccountValidationWeightingRepository : Repository<AccountValidationWeighting>, IAccountValidationWeightingRepository
    {
        public AccountValidationWeightingRepository(IncomeDbContext context) : base(context)
        {
        }
    }
}