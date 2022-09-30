using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.MetadataKey;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class MetadataKeyRepository : Repository<MetadataKey>, IMetadataKeyRepository
    {
        public MetadataKeyRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public IEnumerable<MetadataKey> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.MetadataKeys
                .AsQueryable();

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                items = items.Where(x => x.Name.Contains(criteria.Name));
            }

            if (criteria.EntityType.HasValue)
            {
                items = items.Where(x => x.EntityType == (byte)criteria.EntityType);
            }

            items = items.ApplyFilters(Filters);

            resultCount = items.Count();

            if (criteria.ApplyPaging)
            {

                if (criteria.PageSize == 0) criteria.PageSize = 20;
                if (criteria.Page == 0) criteria.Page = 1;

                items = items
                    .OrderBy(x => x.Id)
                    .Skip((criteria.Page - 1) * criteria.PageSize)
                    .Take(criteria.PageSize);
            }

            return items.ToList();
        }

        public MetadataKey Get(int id)
        {
            var item = IncomeDbContext.MetadataKeys
                .AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public void Update(MetadataKey entity)
        {
            var item = IncomeDbContext.MetadataKeys
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.Name = entity.Name;
            item.Description = entity.Description;
        }
    }
}