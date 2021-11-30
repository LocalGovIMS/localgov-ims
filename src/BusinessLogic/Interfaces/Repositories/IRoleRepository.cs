using BusinessLogic.Entities;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IRoleRepository : IRepository<Role>
    {
        Role GetRole(int id);
        void Update(Role entity);
    }
}