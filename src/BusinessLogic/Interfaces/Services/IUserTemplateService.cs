using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IUserTemplateService
    {
        List<UserTemplate> GetUserTemplates(int id);
        List<string> GetUserTemplates(string userName);
        IResult Update(List<UserTemplate> roles, int userId);
    }
}