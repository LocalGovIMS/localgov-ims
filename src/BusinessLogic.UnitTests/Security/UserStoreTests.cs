using BusinessLogic.Interfaces.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Security
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UserStoreTests
    {
        private readonly Mock<IUserRoleRepository> _mockUserRoleRepository = new Mock<IUserRoleRepository>();
        private readonly Mock<IUserRepository> _mockUserRepository = new Mock<IUserRepository>();
        private readonly Mock<IFundGroupFundRepository> _mockFundGroupFundRepository = new Mock<IFundGroupFundRepository>();
        private readonly Mock<IUserTemplateRepository> _mockUserTemplateRepository = new Mock<IUserTemplateRepository>();
        private readonly Mock<IFundRepository> _mockFundRepository = new Mock<IFundRepository>();

        private BusinessLogic.Security.UserStore GetUserStore()
        {
            return new BusinessLogic.Security.UserStore(
                _mockUserRoleRepository.Object,
                _mockUserRepository.Object,
                _mockFundGroupFundRepository.Object,
                _mockUserTemplateRepository.Object,
                _mockFundRepository.Object);
        }

        [TestMethod]
        public void CanBeInstantiated()
        {
            // Arrange

            try
            {
                // Act
                var userStore = GetUserStore();
            }
            catch (Exception)
            {
                // Assert
                Assert.Fail();
            }
        }

        [TestMethod]
        public void CanGetAUser()
        {
            // Arrange
            _mockUserRepository.Setup(x => x.GetUser(It.IsAny<string>()))
                .Returns(new Entities.User()
                {
                    UserId = 1
                });
            var userStore = GetUserStore();

            // Act
            var result = userStore.GetUser("A user");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Entities.User));
            Assert.AreEqual(result.UserId, 1);
        }

        [TestMethod]
        public void CanGetUserRoles()
        {
            // Arrange
            _mockUserRoleRepository.Setup(x => x.GetByUsername(It.IsAny<string>(), It.IsAny<bool>()))
                .Returns(new List<string>()
                {
                    "Role1", "Role2"
                });
            var userStore = GetUserStore();

            // Act
            var result = userStore.GetUserRoles("A user");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(string[]));
            Assert.IsTrue(result.Length == 2);
        }

        [TestMethod]
        public void CanGetUserFunds()
        {
            // Arrange
            _mockUserRepository.Setup(x => x.GetUserAccessibleFunds(It.IsAny<string>()))
               .Returns(new List<string> ()
               {
                   "F1", "A1"
               });

            var userStore = GetUserStore();

            // Act
            var result = userStore.GetUserFunds("A user");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(string[]));
            Assert.IsTrue(result.Length == 2);
        }

        [TestMethod]
        public void ErrorGettingUserFundsReturnsEmptyArray()
        {
            // Arrange
            var userStore = GetUserStore();

            // Act
            var result = userStore.GetUserFunds("a user to fail");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(string[]));
            Assert.IsTrue(result.Length == 0);
        }

        [TestMethod]
        public void CanGetUserTemplate()
        {
            // Arrange
            _mockUserTemplateRepository.Setup(x => x.GetByUsername(It.IsAny<string>()))
                .Returns(new List<string>()
                {
                    "Template1", "Template2"
                });
            var userStore = GetUserStore();

            // Act
            var result = userStore.GetUserTemplates("A user");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(string[]));
        }
    }
}
