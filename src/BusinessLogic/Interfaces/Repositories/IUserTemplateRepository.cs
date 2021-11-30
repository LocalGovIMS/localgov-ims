using BusinessLogic.Entities;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Repositories
{
    public interface IUserTemplateRepository : IRepository<UserTemplate>
    {
        List<UserTemplate> GetUserTemplates(int id);
        List<string> GetUserTemplates(string userName);
        void UpdateUserTemplates(List<UserTemplate> items, int userId);
    }
}