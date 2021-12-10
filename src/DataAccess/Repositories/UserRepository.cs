using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.User;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;


namespace DataAccess.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(IncomeDbContext context) : base(context)
        {
        }

        public List<User> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.ImsUsers.AsQueryable();

            if (!string.IsNullOrEmpty(criteria.UserName))
            {
                items = items.Where(x => x.UserName.Contains(criteria.UserName));
            }

            if (!string.IsNullOrEmpty(criteria.DisplayName))
            {
                items = items.Where(x => x.DisplayName.Contains(criteria.DisplayName));
            }

            if (criteria.PageSize == 0) criteria.PageSize = 20;
            if (criteria.Page == 0) criteria.Page = 1;

            items = items.ApplyFilters(Filters);

            resultCount = items.Count();

            items = items
                .OrderBy(x => x.UserName)
                .Skip((criteria.Page - 1) * criteria.PageSize)
                .Take(criteria.PageSize);

            return items.ToList();
        }

        public User GetUser(string userName)
        {
            var item = IncomeDbContext.ImsUsers
                .AsQueryable()
                .Where(x => x.UserName == @userName)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public User GetUser(int id)
        {
            var item = IncomeDbContext.ImsUsers
                .AsQueryable()
                .Where(x => x.UserId == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public void Update(User entity)
        {
            var item = IncomeDbContext.ImsUsers
                .AsQueryable()
                .Where(x => x.UserId == entity.UserId)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            // If we're enabling the account, set the datetime that we've enabled it.
            if ((entity.Disabled != item.Disabled) && entity.Disabled == false) entity.LastEnabledAt = DateTime.Now;

            item.UserName = entity.UserName;
            item.ExpiryDays = entity.ExpiryDays;
            item.Disabled = entity.Disabled;
            item.DisplayName = entity.DisplayName;
            item.OfficeCode = entity.OfficeCode;
            item.LastEnabledAt = entity.LastEnabledAt;
        }

        public void RecordLogin(string userName)
        {
            var item = GetUser(userName);

            item.LastLogin = DateTime.Now;
        }

        public void DisableUser(string userName)
        {
            var item = GetUser(userName);

            item.Disabled = true;
        }

        public bool IsDisabled(string userName)
        {
            var item = IncomeDbContext.ImsUsers
                .AsQueryable()
                .Where(x => x.UserName == @userName)
                .ApplyFilters(Filters)
                .Select(x => x.Disabled)
                .FirstOrDefault();

            return item;
        }
    }
}