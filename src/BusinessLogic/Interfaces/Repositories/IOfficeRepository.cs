using BusinessLogic.Entities;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IOfficeRepository : IRepository<Office>
    {
        void Update(Office entity);
    }
}
