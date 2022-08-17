using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.Import;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ImportRepository : Repository<Import>, IImportRepository
    {
        public ImportRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public Import Get(int id)
        {
            var item = IncomeDbContext.Imports
                .Include(x => x.EventLogs)
                .Include(x => x.ImportType)
                .Include(x => x.StatusHistories)
                .Include(x => x.StatusHistories.Select(y => y.CreatedByUser))
                .Include(x => x.CreatedByUser)
                .AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public List<Import> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.Imports
                .Include(x => x.EventLogs)
                .Include(x => x.ImportType)
                .Include(x => x.StatusHistories)
                .AsQueryable();

            if (criteria.DataType.HasValue)
            {
                items = items.Where(x => x.ImportType.DataType == criteria.DataType.Value);
            }

            if (criteria.ImportTypeId.HasValue)
            {
                items = items.Where(x => x.ImportTypeId == criteria.ImportTypeId.Value);
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