using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IUserRoleService
    {
        List<UserRole> GetUserRoles(int id);
        List<string> GetUserRoles(string userName);
        IResult Update(List<UserRole> roles, int userId);
    }
}