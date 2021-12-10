using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Repositories
{
    class OfficeRepository : Repository<Office>, IOfficeRepository
    {
        public OfficeRepository(IncomeDbContext context) : base(context)
        {
        }

        public void Update(Office entity)
        {
            var item = IncomeDbContext.Offices
                .AsQueryable()
                .Where(x => x.OfficeCode == entity.OfficeCode)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            item.Name = entity.Name;
        }

        public new IEnumerable<Office> GetAll()
        {
            return IncomeDbContext.Offices.ToList();
        }
    }
}
