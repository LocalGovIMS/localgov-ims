using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.FundMessageMetadata;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class FundMessageMetadataRepository : Repository<FundMessageMetadata>, IFundMessageMetadataRepository
    {
        public FundMessageMetadataRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public IEnumerable<FundMessageMetadata> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.FundMessageMetadata
                .Include(x => x.MetadataKey)
                .AsQueryable();

            items = items.Where(x => x.FundMessageId == criteria.FundMessageId);

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

        public FundMessageMetadata Get(int id)
        {
            var item = IncomeDbContext.FundMessageMetadata
                .Include(x => x.MetadataKey)
                .AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public void Update(FundMessageMetadata entity)
        {
            var item = IncomeDbContext.FundMessageMetadata
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.FundMessageId = entity.FundMessageId;
            item.MetadataKeyId = entity.MetadataKeyId;
            item.Value = entity.Value;
        }
    }
}