using BusinessLogic.Interfaces.Security;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;

namespace BusinessLogic.UnitTests.Helpers
{
    // This class allows us to check that the correct permissions are set with a method 

    [ExcludeFromCodeCoverage]
    public class PermissionTestHelper
    {
        private readonly Mock<ISecurityContext> _securityContext;
        private readonly List<string> _roles;
        private readonly Action _roleCheck;
        private readonly Action _otherRoleCheck;

        public PermissionTestHelper(
            Mock<ISecurityContext> securityContext,
            List<string> roles,
            Action roleCheck,
            Action otherRoleCheck)
        {
            _securityContext = securityContext;
            _roles = roles;
            _roleCheck = roleCheck;
            _otherRoleCheck = otherRoleCheck;
        }

        public void CheckRoles()
        {
            var otherRoles = GetConstants(typeof(BusinessLogic.Security.Role)).Except(_roles);

            // Check correct roles pass
            foreach (var role in _roles)
            {
                SetupSecurityContext(role, true);

                _roleCheck();
            }

            // Check other roles fail
            foreach (var role in otherRoles)
            {
                SetupSecurityContext(role, false);

                _otherRoleCheck();
            }
        }

        private void SetupSecurityContext(string role, bool result)
        {
            _securityContext.Reset();
            _securityContext.Setup(x => x.IsInRole(role)).Returns(result);
        }

        private static List<string> GetConstants(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly).Select(x => x.GetRawConstantValue().ToString()).ToList();
        }
    }
}
