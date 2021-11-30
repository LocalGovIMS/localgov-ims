using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.User
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DoesUserAccountNeedDisablingTests : BaseUserTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Users.GetUser(It.IsAny<string>()))
               .Returns(new Entities.User());

            MockUnitOfWork.Setup(x => x.Users.IsDisabled(It.IsAny<string>())).Returns(true);
        }

        private Entities.User GetUser()
        {
            return new Entities.User()
            {
                LastLogin = DateTime.Now,
                LastEnabledAt = DateTime.Now,
                CreatedAt = DateTime.Now,
                ExpiryDays = 1
            };
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.DoesUserAccountNeedDisabling(GetUser());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
        }

        [TestMethod]
        public void SaveReturnsSuccess()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.DoesUserAccountNeedDisabling(GetUser());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void SaveErrorReturnsFailure()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.DoesUserAccountNeedDisabling(GetUser());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void NullUserReturnsFailure()
        {
            // Arrange
            MockUnitOfWork.Setup(x => x.Users.GetUser(It.IsAny<string>())).Returns((Entities.User)null);

            var service = GetService();

            // Act
            var result = service.DoesUserAccountNeedDisabling(GetUser());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.AreEqual(result, false);
        }

        [TestMethod]
        public void ErrorGettingUserReturnsFailure()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.DoesUserAccountNeedDisabling(GetUser());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
            Assert.AreEqual(result, false);
        }
    }
}
