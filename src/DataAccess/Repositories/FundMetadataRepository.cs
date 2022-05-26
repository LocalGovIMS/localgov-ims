using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.FundMetadata;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class FundMetadataRepository : Repository<FundMetadata>, IFundMetadataRepository
    {
        public FundMetadataRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public IEnumerable<FundMetadata> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.FundMetadata
                .AsQueryable();

            items = items.Where(x => x.FundCode == criteria.FundCode);

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

        public FundMetadata Get(int id)
        {
            var item = IncomeDbContext.FundMetadata
                .AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public FundMetadata Get(string fundCode, string key)
        {
            var item = IncomeDbContext.FundMetadata
                .AsQueryable()
                .Where(x => x.FundCode == fundCode && x.Key == key)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public void Update(FundMetadata entity)
        {
            var item = IncomeDbContext.FundMetadata
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.FundCode = entity.FundCode;
            item.Key = entity.Key;
            item.Value = entity.Value;
        }
    }
}