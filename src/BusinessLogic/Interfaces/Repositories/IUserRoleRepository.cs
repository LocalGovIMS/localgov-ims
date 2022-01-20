using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IUserRoleRepository : IRepository<UserRole>
    {
        List<UserRole> GetUserRoles(int id);
        List<string> GetUserRoles(string userName, bool track);
        void UpdateUserRoles(List<UserRole> roles, int userId);
    }
}