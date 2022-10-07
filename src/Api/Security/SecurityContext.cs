using BusinessLogic.Interfaces.Security;
using BusinessLogic.Security;
using System.Linq;

namespace Api.Security
{
    public class SecurityContext : ISecurityContext
    {
        public SecurityContext()
        {
        }

        public string Username
        {
            get
            {
                return "0";
            }
        }

        public int UserId { get { return 0; } }

        public bool IsInRole(string role)
        {
            string[] roles = new string[]
            {
                Role.TransactionList,
                Role.TransactionDetails,
                Role.TransactionCreate,
                Role.TransactionEdit,
                Role.TransactionSave,
                Role.Payments
            };

            return roles.Contains(role);
        }

        public bool IsSuperUser
        {
            get
            {
                return true;
            }
        }

        public bool IsPublicUser
        {
            get
            {
                return true;
            }
        }

        public string[] AccessibleFundCodes
        {
            get
            {
                return new string[] { };
            }
        }

        public int[] AccessibleTemplates
        {
            get
            {
                return new int[] { };
            }
        }

        public string OfficeCode
        {
            get
            {
                return "99";
            }
        }
    }
}