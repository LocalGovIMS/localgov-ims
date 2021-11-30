using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Repositories;
using DataAccess.Extensions;
using DataAccess.Persistence;
using System.Linq;

namespace DataAccess.Repositories
{
    public class RoleRepository : Repository<Role>, IRoleRepository
    {
        public RoleRepository(IncomeDbContext context) : base(context)
        {
            IncomeDbContext.Configuration.ProxyCreationEnabled = false;
            IncomeDbContext.Configuration.LazyLoadingEnabled = false;
        }

        public Role GetRole(int id)
        {
            var item = IncomeDbContext.ImsRoles
                .AsQueryable()
                .Where(x => x.RoleId == id)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            return item;
        }

        public void Update(Role entity)
        {
            var role = IncomeDbContext.ImsRoles
                .AsQueryable()
                .Where(x => x.RoleId == entity.RoleId)
                .ApplyFilters(Filters)
                .FirstOrDefault();

            role.DisplayName = entity.DisplayName;
        }
    }
}