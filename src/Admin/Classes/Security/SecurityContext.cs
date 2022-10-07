using BusinessLogic.Interfaces.Security;
using BusinessLogic.Security;
using System.Web;

namespace Admin.Classes.Security
{
    public class SecurityContext : ISecurityContext
    {
        private readonly IUserStore _userStore;

        public SecurityContext(IUserStore userStore)
        {
            _userStore = userStore;
        }

        public string Username
        {
            get
            {
                return HttpContext.Current.User.Identity.Name;
            }
        }

        public int UserId
        {
            get { return _userStore.GetUser(Username).UserId; }
        }

        public bool IsInRole(string role)
        {
            return HttpContext.Current.User.IsInRole(role);
        }

        public bool IsSuperUser
        {
            get
            {
                return IsInRole(Role.SuperUser);
            }
        }

        public bool IsPublicUser
        {
            get
            {
                return false;
            }
        }

        public string[] AccessibleFundCodes
        {
            get
            {
                return _userStore.GetUserFunds(Username);
            }
        }

        public int[] AccessibleTemplates
        {
            get
            {
                return _userStore.GetUserTemplates(Username);
            }
        }

        public string OfficeCode
        {
            get
            {
                return _userStore.GetUser(Username).OfficeCode;
            }
        }
    }
}