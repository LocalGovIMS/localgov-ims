using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IUserFundGroupService
    {
        List<UserFundGroup> GetByUserId(int id);
        IResult Update(List<UserFundGroup> items, int userId);
    }
}