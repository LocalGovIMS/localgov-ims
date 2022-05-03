using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IUserTemplateRepository : IRepository<UserTemplate>
    {
        List<UserTemplate> GetByUserId(int id);
        List<string> GetByUsername(string userName);
        void Update(List<UserTemplate> items, int userId);
    }
}