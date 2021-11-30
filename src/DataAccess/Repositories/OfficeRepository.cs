using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
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

        public new IEnumerable<Office> GetAll()
        {
            return IncomeDbContext.Offices.ToList();
        }
    }
}
