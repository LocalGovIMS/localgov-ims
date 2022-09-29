using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.MethodOfPaymentMetadataKey;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class MethodOfPaymentMetadataKeyRepository : Repository<MopMetadataKey>, IMethodOfPaymentMetadataKeyRepository
    {
        public MethodOfPaymentMetadataKeyRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public IEnumerable<MopMetadataKey> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.MopMetadataKeys
                .AsQueryable();

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                items = items.Where(x => x.Name == criteria.Name);
            }

            if(criteria.Type.HasValue)
            {
                items = items.Where(x => x.Type == (byte)criteria.Type);
            }

            if (criteria.PageSize == 0) criteria.PageSize = 20;
            if (criteria.Page == 0) criteria.Page = 1;

            items = items.ApplyFilters(Filters);

            resultCount = items.Count();

            items = items
                .OrderBy(x => x.Id)
                .Skip((criteria.Page - 1) * criteria.PageSize)
                .Take(criteria.PageSize);

            return items.ToList();
        }

        public MopMetadataKey Get(int id)
        {
            var item = IncomeDbContext.MopMetadataKeys
                .AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public void Update(MopMetadataKey entity)
        {
            var item = IncomeDbContext.MopMetadataKeys
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.Name = entity.Name;
            item.Description = entity.Description;
        }
    }
}