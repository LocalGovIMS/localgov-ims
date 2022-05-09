using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class UserFundGroupRepository : Repository<UserFundGroup>, IUserFundGroupRepository
    {
        public UserFundGroupRepository(IncomeDbContext context) : base(context)
        {
        }

        public List<UserFundGroup> GetByUserId(int id)
        {
            var items = DbSet
                .Include(x => x.FundGroup)
                .ApplyFilters(Filters)
                .Where(x => x.UserId == id).ToList();

            items.ForEach(x => x.User = null);
            items.ForEach(x => x.FundGroup.UserFundGroups = null);

            return items;
        }

        public void Update(List<UserFundGroup> items, int userId)
        {
            var filteredItems = items
                .AsQueryable()
                .ApplyFilters(Filters);

            var existingItems = IncomeDbContext.ImsUserFundGroups
                .Where(x => x.UserId == userId)
                .ApplyFilters(Filters)
                .ToList();

            #region Delete every role

            if (items == null || items.Count < 1)
            {
                foreach (var item in existingItems)
                {
                    IncomeDbContext.ImsUserFundGroups.Remove(item);
                }

                return;
            }

            #endregion

            #region Delete UserRoles

            var toKeep = from a in existingItems
                         join b in filteredItems
                         on new { a.FundGroupId, a.UserId } equals new { b.FundGroupId, b.UserId }
                         select a;

            var toRemove = existingItems.Except(toKeep);

            foreach (var item in toRemove)
            {
                IncomeDbContext.ImsUserFundGroups.Remove(item);
            }

            #endregion

            #region Add UserRoles

            var alreadyExist = from a in filteredItems
                               join b in existingItems
                               on new { a.FundGroupId, a.UserId } equals new { b.FundGroupId, b.UserId }
                               select a;

            var toAdd = items.Except(alreadyExist);

            foreach (var item in toAdd)
            {
                IncomeDbContext.ImsUserFundGroups.Add(item);
            }

            #endregion
        }
    }
}