using BusinessLogic.Interfaces.Security;
using System;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Admin.Classes.Security
{
    /// <summary>
    /// Performs all user and role management
    /// </summary>
    public class PaymentsAdminRoleProvider : RoleProvider
    {
        private readonly IUserStore _userRoleStore;

        public PaymentsAdminRoleProvider()
        {
            _userRoleStore = DependencyResolver.Current.GetService<IUserStore>();
        }

        /// <summary>
        /// Get all the roles for the user specified
        /// </summary>
        /// <param name="username">The username to search for</param>
        /// <returns></returns>
        public override string[] GetRolesForUser(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException(string.Empty, "username");
            }

            return _userRoleStore.GetUserRoles(username).ToArray();
        }

        /// <summary>
        /// Checks whether the specified username is part of the specified role name
        /// </summary>
        /// <param name="username">The username to search for</param>
        /// <param name="roleName">The role name to search for</param>
        /// <returns></returns>
        public override bool IsUserInRole(string username, string roleName)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentException(string.Empty, "username");
            if (string.IsNullOrWhiteSpace(roleName)) throw new ArgumentException(string.Empty, "roleName");

            var roles = GetRolesForUser(username);

            return roles.Any(o => string.Equals(o, roleName, StringComparison.InvariantCultureIgnoreCase));
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}