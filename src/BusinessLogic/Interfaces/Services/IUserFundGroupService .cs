using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IUserFundGroupService
    {
        List<UserFundGroup> GetUserFundGroups(int id);
        IResult Update(List<UserFundGroup> items, int userId);
    }
}