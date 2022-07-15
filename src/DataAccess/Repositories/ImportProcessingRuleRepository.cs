using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.ImportProcessingRule;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ImportProcessingRuleRepository : Repository<ImportProcessingRule>, IImportProcessingRuleRepository
    {
        public ImportProcessingRuleRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public IEnumerable<ImportProcessingRule> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.ImportProcessingRules
                .Include(x => x.Conditions)
                .Include(x => x.Conditions.Select(y => y.Field))
                .Include(x => x.Conditions.Select(y => y.Operator))
                .Include(x => x.Actions)
                .Include(x => x.Actions.Select(y => y.Field))
                .Include(x => x.ImportTypes)
                .AsQueryable();

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                items = items.Where(x => x.Name.Contains(criteria.Name));
            }

            if (criteria.PageSize == 0) criteria.PageSize = 20;
            if (criteria.Page == 0) criteria.Page = 1;

            items = items.ApplyFilters(Filters);

            resultCount = items.Count();

            items = items
                .OrderBy(x => x.Name)
                .Skip((criteria.Page - 1) * criteria.PageSize)
                .Take(criteria.PageSize);

            return items.ToList();
        }

        public ImportProcessingRule Get(int id)
        {
            var item = IncomeDbContext.ImportProcessingRules.AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .Include(x => x.Conditions)
                .Include(x => x.Conditions.Select(y => y.Field))
                .Include(x => x.Conditions.Select(y => y.Operator))
                .Include(x => x.Actions)
                .Include(x => x.Actions.Select(y => y.Field))
                .Include(x => x.ImportTypes)
                .FirstOrDefault();

            return item;
        }

        public IEnumerable<ImportProcessingRule> GetAll(bool includeDisabled)
        {
            var items = IncomeDbContext.ImportProcessingRules.AsQueryable()
                .Where(x => x.Disabled == false || includeDisabled == true)
                .ApplyFilters(Filters)
                .Include(x => x.Conditions)
                .Include(x => x.Conditions.Select(y => y.Field))
                .Include(x => x.Conditions.Select(y => y.Operator))
                .Include(x => x.Actions)
                .Include(x => x.Actions.Select(y => y.Field))
                .Include(x => x.ImportTypes)
                .ToList();

            return items;
        }

        public void Update(ImportProcessingRule entity)
        {
            var item = IncomeDbContext.ImportProcessingRules
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.Name = entity.Name;
            item.Description = entity.Description;
            item.Disabled = entity.Disabled;
        }
    }
}