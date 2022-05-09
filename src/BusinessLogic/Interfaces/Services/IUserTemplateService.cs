using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using System.Collections.Generic;

namespace BusinessLogic.Interfaces.Services
{
    public interface IUserTemplateService
    {
        List<UserTemplate> GetByUserId(int id);
        List<string> GetByUsername(string userName);
        IResult Update(List<UserTemplate> roles, int userId);
    }
}