using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Persistence;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class AccountReferenceValidatorRepository : Repository<AccountReferenceValidator>, IAccountReferenceValidatorRepository
    {
        public AccountReferenceValidatorRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public AccountReferenceValidator GetByFundCode(string fundCode)
        {
            var accountReferenceValidatorId = IncomeDbContext.Funds.FirstOrDefault(x => x.FundCode == fundCode).AccountReferenceValidatorId;

            return IncomeDbContext.AccountReferenceValidators
                .Include(c => c.CheckDigitConfiguration)
                .FirstOrDefault(x => x.Id == accountReferenceValidatorId);
        }
    }
}