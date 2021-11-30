using BusinessLogic.Entities;

namespace BusinessLogic.Interfaces.Security
{
    public interface IUserStore
    {
        string[] GetUserRoles(string userName);
        string[] GetUserFunds(string userName);
        string[] GetUserTemplates(string userName);
        string[] GetFundsForOrigin(string origin);
        User GetUser(string userName);
    }
}
