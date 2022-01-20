using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class UserRoleRepository : Repository<UserRole>, IUserRoleRepository
    {

        public UserRoleRepository(IncomeDbContext context) : base(context)
        {
        }

        public List<UserRole> GetUserRoles(int id)
        {
            var results = IncomeDbContext.ImsUserRoles
                .AsQueryable()
                .Include(x => x.Role)
                .Where(x => x.UserId == id)
                .ApplyFilters(Filters)
                .ToList();

            results.ForEach(x => x.User = null);
            results.ForEach(x => x.Role.UserRoles = null);

            return results;
        }

        public List<string> GetUserRoles(string userName, bool track = true)
        {
            var userRoles = new List<UserRole>();

            userRoles = IncomeDbContext.ImsUserRoles
                .AsQueryable()
                .AsNoTracking(track)
                .Include(x => x.Role)
                .Include(x => x.User)
                .Where(x => x.User.UserName == @userName)
                .ApplyFilters(Filters)
                .ToList();

            userRoles.ForEach(x => x.User = null);

            return userRoles.Select(x => x.Role.Name).ToList();
        }

        public void UpdateUserRoles(List<UserRole> items, int userId)
        {
            var filteredItems = items
                .AsQueryable()
                .ApplyFilters(Filters);

            var userRoles = IncomeDbContext.ImsUserRoles
                .Where(x => x.UserId == userId)
                .ApplyFilters(Filters)
                .ToList();

            #region Delete every role

            if (items == null || items.Count < 1)
            {
                foreach (var role in userRoles)
                {
                    IncomeDbContext.ImsUserRoles.Remove(role);
                }

                return;
            }

            #endregion

            #region Delete UserRoles

            var toKeep = from a in userRoles
                         join b in filteredItems
                         on new { a.RoleId, a.UserId } equals new { b.RoleId, b.UserId }
                         select a;

            var toRemove = userRoles.Except(toKeep);

            foreach (var role in toRemove)
            {
                IncomeDbContext.ImsUserRoles.Remove(role);
            }

            #endregion

            #region Add UserRoles

            var alreadyExist = from a in filteredItems
                               join b in userRoles
                               on new { a.RoleId, a.UserId } equals new { b.RoleId, b.UserId }
                               select a;

            var toAdd = items.Except(alreadyExist);

            foreach (var item in toAdd)
            {
                IncomeDbContext.ImsUserRoles.Add(item);
            }

            #endregion

        }
    }
}