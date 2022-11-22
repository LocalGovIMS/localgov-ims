using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using BusinessLogic.Models.EReturnTemplate;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class EReturnTemplateRepository : Repository<Template>, IEReturnTemplateRepository
    {
        public EReturnTemplateRepository(IncomeDbContext context) : base(context)
        {
        }

        public IEnumerable<Template> Search(SearchCriteria criteria, out int resultCount)
        {
            var items = IncomeDbContext.Templates
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

        public Template Get(int id)
        {
            var item = IncomeDbContext.Templates
                .Include(x => x.TemplateRows)
                .AsQueryable()
                .Where(x => x.Id == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public void Update(Template entity)
        {
            var item = IncomeDbContext.Templates
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.Name = entity.Name;
            item.Cash = entity.Cash;
            item.Cheque = entity.Cheque;
            item.Pdq = entity.Pdq;
        }
    }
}