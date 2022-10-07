using BusinessLogic.Entities;

namespace BusinessLogic.Interfaces.Security
{
    public interface IUserStore
    {
        string[] GetUserRoles(string username);
        string[] GetUserFunds(string username);
        int[] GetUserTemplates(string username);
        string[] GetFundsForOrigin(string origin);
        User GetUser(string username);
    }
}
