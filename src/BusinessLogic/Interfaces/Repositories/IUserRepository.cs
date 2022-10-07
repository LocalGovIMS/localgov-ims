using BusinessLogic.Entities;
using BusinessLogic.Models.User;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        List<User> Search(SearchCriteria criteria, out int resultCount);
        User GetUser(string username);
        User GetUser(int id);
        void Update(User entity);
        void RecordLogin(string username);
        void DisableUser(string username);
        bool IsDisabled(string username);
        List<string> GetUserAccessibleFunds(string username);
    }
}