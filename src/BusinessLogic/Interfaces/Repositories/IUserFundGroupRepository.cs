using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IUserFundGroupRepository : IRepository<UserFundGroup>
    {
        List<UserFundGroup> GetByUserId(int id);
        void Update(List<UserFundGroup> roles, int userId);
    }
}