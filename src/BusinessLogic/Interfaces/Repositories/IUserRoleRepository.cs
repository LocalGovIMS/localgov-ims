using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        List<UserRole> GetByUserId(int id);
        List<string> GetByUsername(string userName, bool track);
        void Update(List<UserRole> roles, int userId);
    }
}