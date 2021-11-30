using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.UserRole
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetUserRolesIntTests : BaseUserRoleTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.UserRoles.GetUserRoles(It.IsAny<int>()))
                .Returns(new List<Entities.UserRole>() {
                    new Entities.UserRole()
                });
        }

        private void SetupSecurityContext(bool result)
        {
            MockSecurityContext.Setup(x => x.IsInRole(It.IsAny<string>()))
                .Returns(result);
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetUserRoles(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Entities.UserRole>));
        }

        [TestMethod]
        public void OnErrorReturnsEmptyList()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetUserRoles(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Entities.UserRole>));
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsEmptyList()
        {
            // Arrange
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.GetUserRoles(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Entities.UserRole>));
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void CheckPermissions()
        {
            // Arrange
            var roles = new List<string>() { BusinessLogic.Security.Role.SystemAdmin };
            var service = GetService();
            SetupUnitOfWork();

            var helper = new PermissionTestHelper(
                MockSecurityContext,
                roles,
                () =>
                {
                    // Act
                    var result = service.GetUserRoles(1);

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(List<Entities.UserRole>));
                },
                () =>
                {
                    // Act
                    var result = service.GetUserRoles(1);

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(List<Entities.UserRole>));
                    Assert.AreEqual(result.Count, 0);
                });

            // Act
            helper.CheckRoles();
        }
    }
}
