using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IUserMopCodeRepository : IRepository<UserMopCode>
    {
        List<UserMopCode> GetByUserId(int id);
        void Update(List<UserMopCode> userMopCodes, int userId);
    }
}