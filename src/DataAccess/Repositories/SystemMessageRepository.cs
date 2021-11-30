using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Classes.Caching;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class SystemMessageRepository : Repository<SystemMessage>, ISystemMessageRepository
    {
        public SystemMessageRepository(IncomeDbContext context) : base(context)
        {
        }

        public SystemMessage GetSystemMessage(int id)
        {
            var item = IncomeDbContext.SystemMessages
                .AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public void Update(SystemMessage entity)
        {
            var item = IncomeDbContext.SystemMessages
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.Severity = entity.Severity;
            item.StartDate = entity.StartDate;
            item.EndDate = entity.EndDate;
            item.Message = entity.Message;
        }

        public IEnumerable<SystemMessage> GetActive()
        {
            var cacheKey = "SystemMessageRepository::GetActive";

            var output = MemoryCache.GetCachedData<IEnumerable<SystemMessage>>(
                cacheKey,
                () =>
                {
                    return IncomeDbContext.SystemMessages
                        .AsQueryable()
                        .Where(x => x.StartDate <= DateTime.Now && x.EndDate >= DateTime.Now)
                        .ApplyFilters(Filters).ToList();
                });

            return output;
        }
    }
}