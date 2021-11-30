using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using BusinessLogic.Models.Shared;
using BusinessLogic.Models.User;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IUserService
    {
        SearchResult<User> Search(SearchCriteria criteria);
        List<User> GetAllUsers();
        User GetUser(int id);
        User GetUser(string userName);
        IResult Create(User item);
        IResult Update(User user);
        IResult RecordLogin(string userName);
        bool DoesUserAccountNeedDisabling(User user);
        IResult DisableUser(string userName);
        IResult IsUserDisabled(string userName);
    }
}