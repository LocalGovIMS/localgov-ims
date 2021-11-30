using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class FundGroupFundRepository : Repository<FundGroupFund>, IFundGroupFundRepository
    {
        public FundGroupFundRepository(IncomeDbContext context) : base(context)
        {
        }

        public List<FundGroupFund> GetFundGroupFundsByFundGroupId(int id)
        {
            return IncomeDbContext.FundGroupFunds
                .Where(x => x.FundGroupId == id)
                .ApplyFilters(Filters)
                .ToList();
        }

        public List<FundGroupFund> GetFundGroupFunds(int id)
        {
            var items = IncomeDbContext.FundGroupFunds
                .AsQueryable()
                .Include(x => x.FundGroup)
                .Include(x => x.FundGroup.UserFundGroups)
                .Where(x => x.FundGroupId == id)
                .ApplyFilters(Filters)
                .ToList();

            return items;
        }

        public List<FundGroupFund> GetAllExtended()
        {
            var items = IncomeDbContext.FundGroupFunds
                .AsQueryable()
                .Include(x => x.Fund)
                .Include(x => x.FundGroup)
                .Include(x => x.FundGroup.UserFundGroups)
                .ApplyFilters(Filters)
                .ToList();

            return items;
        }

        public void UpdateFundGroupFunds(List<FundGroupFund> items)
        {
            var filteredItems = items
                .AsQueryable()
                .ApplyFilters(Filters);

            var id = filteredItems.FirstOrDefault().FundGroupId;
            var exitstingItems = IncomeDbContext.FundGroupFunds
                .Where(x => x.FundGroupId == id)
                .ApplyFilters(Filters)
                .ToList();

            #region Delete UserRoles

            var toKeep = from a in exitstingItems
                         join b in filteredItems
                         on new { a.FundGroupId, a.FundGroupFundId } equals new { b.FundGroupId, b.FundGroupFundId }
                         select a;

            var toRemove = exitstingItems.Except(toKeep);

            foreach (var item in toRemove)
            {
                IncomeDbContext.FundGroupFunds.Remove(item);
            }

            #endregion

            #region Add UserRoles

            var alreadyExist = from a in filteredItems
                               join b in exitstingItems
                               on new { a.FundGroupId, a.FundGroupFundId } equals new { b.FundGroupId, b.FundGroupFundId }
                               select a;

            var toAdd = items.Except(alreadyExist);

            foreach (var item in toAdd)
            {
                IncomeDbContext.FundGroupFunds.Add(item);
            }

            #endregion
        }
    }
}