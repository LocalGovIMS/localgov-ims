using Admin.Classes.Security;
using BusinessLogic.Interfaces.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Web.Mvc;

namespace Admin.UnitTests.Classes.Security
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class PaymentsAdminRoleProviderTests
    {
        private Mock<IDependencyResolver> _mockDependencyResolver = new Mock<IDependencyResolver>();
        private Mock<IUserStore> _mockUserStore = new Mock<IUserStore>();

        private void Setup()
        {
            _mockUserStore.Setup(x => x.GetUserRoles(
                It.IsAny<string>()))
                .Returns(new string[] { "1", "2" });

            _mockDependencyResolver.Setup(m => m.GetService(It.IsAny<Type>())).Returns(_mockUserStore.Object);

            DependencyResolver.SetResolver(_mockDependencyResolver.Object);
        }

        [TestMethod]
        public void CanInstantiate()
        {
            // Arrange
            Setup();

            // Act
            var paymentsAdminRoleProvider = new PaymentsAdminRoleProvider();

            // Assert
            Assert.IsNotNull(paymentsAdminRoleProvider);
        }

        [TestMethod]
        public void CanGetRolesForUser()
        {
            // Arrange
            Setup();
            var paymentsAdminRoleProvider = new PaymentsAdminRoleProvider();

            // Act
            var result = paymentsAdminRoleProvider.GetRolesForUser("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(string[]));
            Assert.IsTrue(result.Contains("1"));
            Assert.IsTrue(result.Contains("2"));
            Assert.IsFalse(result.Contains("3"));
        }

        [TestMethod]
        public void CanGetRolesForUserWithoutUsernameCausesException()
        {
            // Arrange
            Setup();
            var paymentsAdminRoleProvider = new PaymentsAdminRoleProvider();

            // Act
            try
            {
                var result = paymentsAdminRoleProvider.GetRolesForUser(string.Empty);
                Assert.Fail();

            }
            catch (Exception e)
            {
                // Assert
                Assert.IsNotNull(e);
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
                Assert.AreEqual(((ArgumentException)e).ParamName, "username");
            }
        }

        [TestMethod]
        public void IsInRoleForValidRoleReturnsTrue()
        {
            // Arrange
            Setup();
            var paymentsAdminRoleProvider = new PaymentsAdminRoleProvider();


            var result = paymentsAdminRoleProvider.IsUserInRole("Test", "1");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void IsInRoleForInvalidRoleReturnsFalse()
        {
            // Arrange
            Setup();
            var paymentsAdminRoleProvider = new PaymentsAdminRoleProvider();

            var result = paymentsAdminRoleProvider.IsUserInRole("Test", "100");

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void IsInRoleForMissingUsernameCausesException()
        {
            // Arrange
            Setup();
            var paymentsAdminRoleProvider = new PaymentsAdminRoleProvider();

            // Act
            try
            {
                var result = paymentsAdminRoleProvider.IsUserInRole(string.Empty, "1");
                Assert.Fail();
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsNotNull(e);
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
                Assert.AreEqual(((ArgumentException)e).ParamName, "username");
            }
        }

        [TestMethod]
        public void IsInRoleForMissingRoleCausesException()
        {
            // Arrange
            Setup();
            var paymentsAdminRoleProvider = new PaymentsAdminRoleProvider();

            // Act
            try
            {
                var result = paymentsAdminRoleProvider.IsUserInRole("Test", string.Empty);
                Assert.Fail();
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsNotNull(e);
                Assert.IsInstanceOfType(e, typeof(ArgumentException));
                Assert.AreEqual(((ArgumentException)e).ParamName, "roleName");
            }
        }

        [TestMethod]
        public void AddUserToRolesIsNotImplemented()
        {
            // Arrange
            Setup();
            var paymentsAdminRoleProvider = new PaymentsAdminRoleProvider();

            // Act
            try
            {
                paymentsAdminRoleProvider.AddUsersToRoles(new string[] { "Test" }, new string[] { "1" });
                Assert.Fail();
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsNotNull(e);
                Assert.IsInstanceOfType(e, typeof(NotImplementedException));
            }
        }

        [TestMethod]
        public void SetApplicationNameIsNotImplemented()
        {
            // Arrange
            Setup();
            var paymentsAdminRoleProvider = new PaymentsAdminRoleProvider();

            // Act
            try
            {
                paymentsAdminRoleProvider.ApplicationName = "Test";
                Assert.Fail();
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsNotNull(e);
                Assert.IsInstanceOfType(e, typeof(NotImplementedException));
            }
        }

        [TestMethod]
        public void GetApplicationNameIsNotImplemented()
        {
            // Arrange
            Setup();
            var paymentsAdminRoleProvider = new PaymentsAdminRoleProvider();

            // Act
            try
            {
                var x = paymentsAdminRoleProvider.ApplicationName;
                Assert.Fail();
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsNotNull(e);
                Assert.IsInstanceOfType(e, typeof(NotImplementedException));
            }
        }

        [TestMethod]
        public void CreateRoleIsNotImplemented()
        {
            // Arrange
            Setup();
            var paymentsAdminRoleProvider = new PaymentsAdminRoleProvider();

            // Act
            try
            {
                paymentsAdminRoleProvider.CreateRole("Test");
                Assert.Fail();
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsNotNull(e);
                Assert.IsInstanceOfType(e, typeof(NotImplementedException));
            }
        }

        [TestMethod]
        public void DeleteRoleIsNotImplemented()
        {
            // Arrange
            Setup();
            var paymentsAdminRoleProvider = new PaymentsAdminRoleProvider();

            // Act
            try
            {
                paymentsAdminRoleProvider.DeleteRole("Test", false);
                Assert.Fail();
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsNotNull(e);
                Assert.IsInstanceOfType(e, typeof(NotImplementedException));
            }
        }

        [TestMethod]
        public void FindUsersInRoleIsNotImplemented()
        {
            // Arrange
            Setup();
            var paymentsAdminRoleProvider = new PaymentsAdminRoleProvider();

            // Act
            try
            {
                paymentsAdminRoleProvider.FindUsersInRole("Test", "Test");
                Assert.Fail();
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsNotNull(e);
                Assert.IsInstanceOfType(e, typeof(NotImplementedException));
            }
        }

        [TestMethod]
        public void GetAllRolesIsNotImplemented()
        {
            // Arrange
            Setup();
            var paymentsAdminRoleProvider = new PaymentsAdminRoleProvider();

            // Act
            try
            {
                paymentsAdminRoleProvider.GetAllRoles();
                Assert.Fail();
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsNotNull(e);
                Assert.IsInstanceOfType(e, typeof(NotImplementedException));
            }
        }

        [TestMethod]
        public void GetUsersInRoleIsNotImplemented()
        {
            // Arrange
            Setup();
            var paymentsAdminRoleProvider = new PaymentsAdminRoleProvider();

            // Act
            try
            {
                paymentsAdminRoleProvider.GetUsersInRole("Test");
                Assert.Fail();
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsNotNull(e);
                Assert.IsInstanceOfType(e, typeof(NotImplementedException));
            }
        }

        [TestMethod]
        public void RemoveUsersFromRolesIsNotImplemented()
        {
            // Arrange
            Setup();
            var paymentsAdminRoleProvider = new PaymentsAdminRoleProvider();

            // Act
            try
            {
                paymentsAdminRoleProvider.RemoveUsersFromRoles(new string[] { "Test" }, new string[] { "1" });
                Assert.Fail();
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsNotNull(e);
                Assert.IsInstanceOfType(e, typeof(NotImplementedException));
            }
        }

        [TestMethod]
        public void RoleExistsIsNotImplemented()
        {
            // Arrange
            Setup();
            var paymentsAdminRoleProvider = new PaymentsAdminRoleProvider();

            // Act
            try
            {
                paymentsAdminRoleProvider.RoleExists("Test");
                Assert.Fail();
            }
            catch (Exception e)
            {
                // Assert
                Assert.IsNotNull(e);
                Assert.IsInstanceOfType(e, typeof(NotImplementedException));
            }
        }
    }
}
