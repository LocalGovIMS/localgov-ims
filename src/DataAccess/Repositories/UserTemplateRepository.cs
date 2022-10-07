using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class UserTemplateRepository : Repository<UserTemplate>, IUserTemplateRepository
    {
        public UserTemplateRepository(IncomeDbContext context) : base(context)
        {
        }

        public List<UserTemplate> GetByUserId(int id)
        {
            var results = IncomeDbContext.ImsUserTemplates
                .AsQueryable()
                .Include(x => x.Template)
                .Where(x => x.UserId == id)
                .ApplyFilters(Filters)
                .ToList();

            return results;
        }

        public List<int> GetByUsername(string username)
        {
            var results = IncomeDbContext.ImsUserTemplates
                .AsQueryable()
                .Include(x => x.Template)
                .Include(x => x.User)
                .Where(x => x.User.UserName == @username)
                .ApplyFilters(Filters)
                .ToList();

            return results.Select(x => x.Template.Id).ToList();
        }

        public void Update(List<UserTemplate> items, int userId)
        {
            var filteredItems = items
                .AsQueryable()
                .ApplyFilters(Filters);

            var existingItems = IncomeDbContext.ImsUserTemplates
                .Where(x => x.UserId == userId)
                .ApplyFilters(Filters)
                .ToList();

            #region Delete every role

            if (items == null || items.Count < 1)
            {
                foreach (var item in existingItems)
                {
                    IncomeDbContext.ImsUserTemplates.Remove(item);
                }

                return;
            }

            #endregion

            #region Delete UserRoles

            var toKeep = from a in existingItems
                         join b in filteredItems
                         on new { a.TemplateId, a.UserId } equals new { b.TemplateId, b.UserId }
                         select a;

            var toRemove = existingItems.Except(toKeep);

            foreach (var role in toRemove)
            {
                IncomeDbContext.ImsUserTemplates.Remove(role);
            }

            #endregion

            #region Add UserRoles

            var alreadyExist = from a in filteredItems
                               join b in existingItems
                               on new { a.TemplateId, a.UserId } equals new { b.TemplateId, b.UserId }
                               select a;

            var toAdd = items.Except(alreadyExist);

            foreach (var item in toAdd)
            {
                IncomeDbContext.ImsUserTemplates.Add(item);
            }

            #endregion
        }
    }
}