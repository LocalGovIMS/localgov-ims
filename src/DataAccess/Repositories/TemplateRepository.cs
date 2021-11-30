using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.Repositories
{
    public class TemplateRepository : Repository<Template>, ITemplateRepository
    {
        public TemplateRepository(IncomeDbContext context) : base(context)
        {
        }

        public Template GetTemplate(int id)
        {
            var item = IncomeDbContext.Templates
                .AsQueryable()
                .Include(x => x.TemplateRows)
                .Include(x => x.TemplateRows.Select(y => y.VAT))
                .FirstOrDefault(x => x.Id == id);

            return item;
        }

        public void Update(Template entity)
        {
            var item = IncomeDbContext.Templates
                .AsQueryable()
                .Where(x => x.Id == entity.Id)
                .Include(x => x.TemplateRows)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.Name = entity.Name;
            item.Cash = entity.Cash;
            item.Cheque = entity.Cheque;
            item.Pdq = entity.Pdq;
        }
    }
}
