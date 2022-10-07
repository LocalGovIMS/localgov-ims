using Admin.Classes.Security;
using Admin.UnitTests.Helpers;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace Admin.UnitTests.Classes.Security
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SecurityContextTests
    {
        private Mock<IUserStore> _mockUserStore = new Mock<IUserStore>();

        private void SetupHttpContext(bool isSuperUser)
        {
            HttpContext.Current = MoqHelper.FakeHttpContext();

            var fakeIdentity = new GenericIdentity("User");

            var principal = isSuperUser
                ? new GenericPrincipal(fakeIdentity, new[] { Role.Dashboard, Role.Payments, Role.SuperUser })
                : new GenericPrincipal(fakeIdentity, new[] { Role.Dashboard, Role.Payments });

            HttpContext.Current.User = principal;
        }

        private void SetupUserStore(Mock<IUserStore> userStore)
        {
            userStore.Setup(x => x.GetUser(
                It.IsAny<string>()))
                .Returns(new BusinessLogic.Entities.User()
                {
                    UserId = 1,
                }
            );

            userStore.Setup(x => x.GetUserFunds(
                It.IsAny<string>()))
                .Returns(new string[]
                {
                    "1", "2"
                }
            );
        }

        [TestMethod]
        public void CanInstantiateSecurityContext()
        {
            // Arrange
            var securityContext = new SecurityContext(_mockUserStore.Object);

            // Assert
            Assert.IsNotNull(securityContext);
        }

        [TestMethod]
        public void IsNotAPublicUser()
        {
            // Arrange
            var securityContext = new SecurityContext(_mockUserStore.Object);

            // Assert
            Assert.IsFalse(securityContext.IsPublicUser);
        }

        [TestMethod]
        public void CanGetAUserName()
        {
            // Arrange
            SetupHttpContext(false);
            var securityContext = new SecurityContext(_mockUserStore.Object);

            // Act
            var result = securityContext.Username;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, "User");
        }

        [TestMethod]
        public void IsUserIsInRoleConfirmsUserIsInRole()
        {
            // Arrange
            SetupHttpContext(false);
            var securityContext = new SecurityContext(_mockUserStore.Object);

            // Act
            var result = securityContext.IsInRole(Role.Payments);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void IsUserIsInRoleConfirmsUserIsNotInRole()
        {
            // Arrange
            SetupHttpContext(false);
            var securityContext = new SecurityContext(_mockUserStore.Object);

            // Act
            var result = securityContext.IsInRole(Role.TransactionCreate);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void NonSuperUserIsNotASuperUser()
        {
            // Arrange
            SetupHttpContext(false);
            var securityContext = new SecurityContext(_mockUserStore.Object);

            // Act
            var result = securityContext.IsSuperUser;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void SuperUserIsASuperUser()
        {
            // Arrange
            SetupHttpContext(true);
            var securityContext = new SecurityContext(_mockUserStore.Object);

            // Act
            var result = securityContext.IsSuperUser;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void CanGetUserId()
        {
            // Arrange
            SetupHttpContext(true);
            SetupUserStore(_mockUserStore);
            var securityContext = new SecurityContext(_mockUserStore.Object);

            // Act
            var result = securityContext.UserId;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result, 1);
        }

        [TestMethod]
        public void CanGetUserFundCodes()
        {
            // Arrange
            SetupHttpContext(true);
            SetupUserStore(_mockUserStore);
            var securityContext = new SecurityContext(_mockUserStore.Object);

            // Act
            var result = securityContext.AccessibleFundCodes;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(string[]));
            Assert.IsTrue(result.Contains("1"));
            Assert.IsTrue(result.Contains("2"));
            Assert.IsFalse(result.Contains("3"));
        }
    }
}
