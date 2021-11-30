using BusinessLogic.Classes.Result;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.User
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DisableUserTests : BaseUserTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Users.GetUser(It.IsAny<string>()))
                .Returns(new Entities.User());

            MockUnitOfWork.Setup(x => x.Users.DisableUser(It.IsAny<string>()));
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.DisableUser("Username");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
        }

        [TestMethod]
        public void SaveReturnsSuccess()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.DisableUser("Username");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, true);
        }

        [TestMethod]
        public void SaveErrorReturnsFailure()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.DisableUser("Username");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public void NullUserReturnsFailure()
        {
            // Arrange
            MockUnitOfWork.Setup(x => x.Users.GetUser(It.IsAny<string>())).Returns((Entities.User)null);

            var service = GetService();

            // Act
            var result = service.DisableUser("Username");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, false);
        }
    }
}
