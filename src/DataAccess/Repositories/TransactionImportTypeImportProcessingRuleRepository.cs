using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.TransactionImportTypeImportProcessingRule;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class TransactionImportTypeImportProcessingRuleRepository : Repository<TransactionImportTypeImportProcessingRule>, ITransactionImportTypeImportProcessingRuleRepository
    {
        public TransactionImportTypeImportProcessingRuleRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public TransactionImportTypeImportProcessingRule Get(int id)
        {
            var item = IncomeDbContext.TransactionImportTypeImportProcessingRules
                .Include(x => x.TransactionImportType)
                .Include(x => x.ImportProcessingRule)
                .AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public IEnumerable<TransactionImportTypeImportProcessingRule> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.TransactionImportTypeImportProcessingRules
                .Include(x => x.TransactionImportType)
                .Include(x => x.ImportProcessingRule)
                .AsQueryable();

            if (criteria.TransactionImportTypeId.HasValue)
            {
                items = items.Where(x => x.TransactionImportTypeId == criteria.TransactionImportTypeId.Value);
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

        public void Update(TransactionImportTypeImportProcessingRule entity)
        {
            var item = IncomeDbContext.TransactionImportTypeImportProcessingRules
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.TransactionImportTypeId = entity.TransactionImportTypeId;
            item.ImportProcessingRuleId = entity.ImportProcessingRuleId;
        }
    }
}