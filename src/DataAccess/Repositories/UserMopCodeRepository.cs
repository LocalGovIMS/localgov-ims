using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class UserMopCodeRepository : Repository<UserMopCode>, IUserMopCodeRepository
    {
        public UserMopCodeRepository(IncomeDbContext context) : base(context)
        {
        }

        public List<UserMopCode> GetByUserId(int id)
        {
            var results = IncomeDbContext.ImsUserMopCodes
                .AsQueryable()
                .Include(x => x.Mop)
                .Where(x => x.UserId == id)
                .ApplyFilters(Filters)
                .ToList();

            return results;
        }

        public void Update(List<UserMopCode> items, int userId)
        {
            var filteredItems = items
                .AsQueryable()
                .ApplyFilters(Filters);

            var existingItems = IncomeDbContext.ImsUserMopCodes
                .Where(x => x.UserId == userId)
                .ApplyFilters(Filters)
                .ToList();

            #region Delete every mop code

            if (items == null || items.Count < 1)
            {
                foreach (var item in existingItems)
                {
                    IncomeDbContext.ImsUserMopCodes.Remove(item);
                }

                return;
            }

            #endregion

            #region Delete user mop codes

            var toKeep = from a in existingItems
                         join b in filteredItems
                         on new { a.MopCode, a.UserId } equals new { b.MopCode, b.UserId }
                         select a;

            var toRemove = existingItems.Except(toKeep);

            foreach (var item in toRemove)
            {
                IncomeDbContext.ImsUserMopCodes.Remove(item);
            }

            #endregion

            #region Add user mop codes

            var alreadyExist = from a in filteredItems
                               join b in existingItems
                               on new { a.MopCode, a.UserId } equals new { b.MopCode, b.UserId }
                               select a;

            var toAdd = items.Except(alreadyExist);

            foreach (var item in toAdd)
            {
                IncomeDbContext.ImsUserMopCodes.Add(item);
            }

            #endregion
        }
    }
}