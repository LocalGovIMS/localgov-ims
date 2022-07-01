using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.TransactionImport;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class TransactionImportRepository : Repository<TransactionImport>, ITransactionImportRepository
    {
        public TransactionImportRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public TransactionImport Get(int id)
        {
            var item = IncomeDbContext.TransactionImports
                .Include(x => x.Rows)
                .Include(x => x.EventLogs)
                .Include(x => x.TransactionImportType)
                .Include(x => x.StatusHistories)
                .Include(x => x.StatusHistories.Select(y => y.CreatedByUser))
                .Include(x => x.CreatedByUser)
                .AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public List<TransactionImport> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.TransactionImports
                .Include(x => x.Rows)
                .Include(x => x.EventLogs)
                .Include(x => x.TransactionImportType)
                .Include(x => x.StatusHistories)
                .AsQueryable();

            if (criteria.TransactionImportTypeId.HasValue)
            {
                items = items.Where(x => x.TransactionImportTypeId == criteria.TransactionImportTypeId.Value);
            }

            if (criteria.StatusId.HasValue)
            {
                items = items.Where(x => x.StatusHistories.OrderByDescending(y => y.CreatedDate).FirstOrDefault().StatusId == criteria.StatusId);
            }

            if (criteria.StartDate != null)
            {
                items = items.Where(x => x.CreatedDate >= criteria.StartDate);
            }

            if (criteria.EndDate != null)
            {
                items = items.Where(x => x.CreatedDate <= criteria.EndDate);
            }

            if (criteria.PageSize == 0) criteria.PageSize = 20;
            if (criteria.Page == 0) criteria.Page = 1;

            items = items.ApplyFilters(Filters);

            resultCount = items.Count();

            items = items
                .OrderByDescending(x => x.CreatedDate)
                .Skip((criteria.Page - 1) * criteria.PageSize)
                .Take(criteria.PageSize);

            return items.ToList();
        }
    }
}