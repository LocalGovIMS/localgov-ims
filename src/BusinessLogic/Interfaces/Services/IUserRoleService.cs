using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IUserRoleService
    {
        List<UserRole> GetByUserId(int id);
        List<string> GetByUserRole(string userName);
        IResult Update(List<UserRole> roles, int userId);
    }
}