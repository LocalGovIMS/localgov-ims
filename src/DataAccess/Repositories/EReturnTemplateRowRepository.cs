using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.EReturnTemplateRow;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    public class EReturnTemplateRowRepository : Repository<TemplateRow>, IEReturnTemplateRowRepository
    {
        public EReturnTemplateRowRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public IEnumerable<TemplateRow> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.TemplateRows
                .AsQueryable();

            items = items.Where(x => x.TemplateId == criteria.EReturnTemplateId);

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

        public void Update(TemplateRow entity)
        {
            var item = IncomeDbContext.TemplateRows
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.Reference = entity.Reference;
            item.VatCode = entity.VatCode;
            item.Description = entity.Description;
            item.VatOverride = entity.VatOverride;
            item.ReferenceOverride = entity.ReferenceOverride;
            item.DescriptionOverride = entity.DescriptionOverride;
        }
    }
}