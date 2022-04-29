using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IUserMopCodeService
    {
        List<UserMopCode> GetByUserId(int id);
        Mop GetDefaultUserMopCode();
    }
}