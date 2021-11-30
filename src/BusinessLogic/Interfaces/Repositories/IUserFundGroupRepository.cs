using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IUserFundGroupRepository : IRepository<UserFundGroup>
    {
        List<UserFundGroup> GetUserFundGroups(int id);
        void UpdateUserFundGroups(List<UserFundGroup> roles, int userId);
    }
}