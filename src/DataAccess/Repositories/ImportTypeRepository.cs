using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.ImportType;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class ImportTypeRepository : Repository<ImportType>, IImportTypeRepository
    {
        public ImportTypeRepository(IncomeDbContext context) : base(context)
        {
        }

        public IEnumerable<ImportType> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.ImportTypes
                .AsQueryable();

            if (criteria.DataType.HasValue)
            {
                items = items.Where(x => x.DataType == (byte)criteria.DataType.Value);
            }

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

        public ImportType Get(int id)
        {
            var item = IncomeDbContext.ImportTypes
                .Include(x => x.ImportProcessingRules)
                .AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public void Update(ImportType entity)
        {
            var item = IncomeDbContext.ImportTypes
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.DataType = entity.DataType;    
            item.Name = entity.Name;
            item.Description = entity.Description;
            item.ExternalReference = entity.ExternalReference;
            item.IsReversible = entity.IsReversible;
        }
    }
}