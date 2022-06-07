using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.TransactionImportType;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class TransactionImportTypeRepository : Repository<TransactionImportType>, ITransactionImportTypeRepository
    {
        public TransactionImportTypeRepository(IncomeDbContext context) : base(context)
        {
        }

        public IEnumerable<TransactionImportType> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.TransactionImportTypes
                .AsQueryable();

            if (!string.IsNullOrEmpty(criteria.Name))
            {
                items = items.Where(x => x.Name.Contains(criteria.Name));
            }

            if (!string.IsNullOrEmpty(criteria.ExternalReference))
            {
                items = items.Where(x => x.ExternalReference.Contains(criteria.ExternalReference));
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

        public TransactionImportType Get(int id)
        {
            var item = IncomeDbContext.TransactionImportTypes
                .Include(x => x.ImportProcessingRules)
                .AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public void Update(TransactionImportType entity)
        {
            var item = IncomeDbContext.TransactionImportTypes
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.Name = entity.Name;
            item.Description = entity.Description;
            item.ExternalReference = entity.ExternalReference;
        }
    }
}