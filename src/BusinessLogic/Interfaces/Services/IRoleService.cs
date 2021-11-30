using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IRoleService
    {
        List<Role> GetAllRoles();
        Role GetRole(int id);
        IResult Update(Role item);
    }
}