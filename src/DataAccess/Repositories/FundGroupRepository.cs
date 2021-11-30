using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class FundGroupRepository : Repository<FundGroup>, IFundGroupRepository
    {
        public FundGroupRepository(IncomeDbContext context) : base(context)
        {
        }

        public FundGroup GetFundGroup(int id)
        {
            return IncomeDbContext.FundGroups
                .Include(s => s.FundGroupFunds)
                .Include(s => s.FundGroupFunds.Select(y => y.Fund))
                .Where(x => x.FundGroupId == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();
        }

        public void Update(FundGroup entity)
        {
            IncomeDbContext.FundGroups.Attach(entity);
        }
    }
}