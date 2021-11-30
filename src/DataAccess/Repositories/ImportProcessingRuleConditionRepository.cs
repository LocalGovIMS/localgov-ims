using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.ImportProcessingRuleCondition;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ImportProcessingRuleConditionRepository : Repository<ImportProcessingRuleCondition>, IImportProcessingRuleConditionRepository
    {
        public ImportProcessingRuleConditionRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }


        public IEnumerable<ImportProcessingRuleCondition> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.ImportProcessingRuleConditions
                .Include(x => x.Field)
                .Include(x => x.Operator)
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

        public ImportProcessingRuleCondition Get(int id)
        {
            var item = IncomeDbContext.ImportProcessingRuleConditions
                .Include(x => x.Field)
                .Include(x => x.Operator)
                .AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public void Update(ImportProcessingRuleCondition entity)
        {
            var item = IncomeDbContext.ImportProcessingRuleConditions
                .Include(x => x.Field)
                .Include(x => x.Operator)
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.ImportProcessingRuleId = entity.ImportProcessingRuleId;
            item.LogicalOperator = entity.LogicalOperator;
            item.ImportProcessingRuleFieldId = entity.ImportProcessingRuleFieldId;
            item.ImportProcessingRuleOperatorId = entity.ImportProcessingRuleOperatorId;
            item.Value = entity.Value;
        }
    }
}