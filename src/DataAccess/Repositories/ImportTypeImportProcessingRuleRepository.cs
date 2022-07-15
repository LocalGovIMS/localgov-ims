using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.ImportTypeImportProcessingRule;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ImportTypeImportProcessingRuleRepository : Repository<ImportTypeImportProcessingRule>, IImportTypeImportProcessingRuleRepository
    {
        public ImportTypeImportProcessingRuleRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public ImportTypeImportProcessingRule Get(int id)
        {
            var item = IncomeDbContext.ImportTypeImportProcessingRules
                .Include(x => x.ImportType)
                .Include(x => x.ImportProcessingRule)
                .AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public IEnumerable<ImportTypeImportProcessingRule> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.ImportTypeImportProcessingRules
                .Include(x => x.ImportType)
                .Include(x => x.ImportProcessingRule)
                .AsQueryable();

            if (criteria.ImportTypeId.HasValue)
            {
                items = items.Where(x => x.ImportTypeId == criteria.ImportTypeId.Value);
            }

            if (criteria.ImportProcessingRuleId.HasValue)
            {
                items = items.Where(x => x.ImportProcessingRuleId == criteria.ImportProcessingRuleId.Value);
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

        public void Update(ImportTypeImportProcessingRule entity)
        {
            var item = IncomeDbContext.ImportTypeImportProcessingRules
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.ImportTypeId = entity.ImportTypeId;
            item.ImportProcessingRuleId = entity.ImportProcessingRuleId;
        }
    }
}