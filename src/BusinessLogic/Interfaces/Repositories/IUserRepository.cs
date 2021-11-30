using BusinessLogic.Entities;
using BusinessLogic.Models.User;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        List<User> Search(SearchCriteria criteria, out int resultCount);
        User GetUser(string userName);
        User GetUser(int id);
        void Update(User entity);
        void RecordLogin(string userName);
        void DisableUser(string userName);
        bool IsDisabled(string userName);
    }
}