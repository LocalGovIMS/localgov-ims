using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.ImportProcessingRuleAction;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ImportProcessingRuleActionRepository : Repository<ImportProcessingRuleAction>, IImportProcessingRuleActionRepository
    {
        public ImportProcessingRuleActionRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public IEnumerable<ImportProcessingRuleAction> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.ImportProcessingRuleActions
                .Include(x => x.Field)
                .AsQueryable();

            items = items.Where(x => x.ImportProcessingRuleId == criteria.ImportProcessingRuleId);

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

        public ImportProcessingRuleAction Get(int id)
        {
            var item = IncomeDbContext.ImportProcessingRuleActions
                .Include(x => x.Field)
                .AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public void Update(ImportProcessingRuleAction entity)
        {
            var item = IncomeDbContext.ImportProcessingRuleActions
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.ImportProcessingRuleId = entity.ImportProcessingRuleId;
            item.ImportProcessingRuleFieldId = entity.ImportProcessingRuleFieldId;
            item.Value = entity.Value;
        }
    }
}