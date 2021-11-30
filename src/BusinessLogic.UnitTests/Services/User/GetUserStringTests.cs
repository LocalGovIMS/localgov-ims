using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.User
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetUserStringTests : BaseUserTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Users.GetUser(It.IsAny<string>()))
                .Returns(new Entities.User());
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
            var result = service.GetUser("user");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Entities.User));
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetUser("user");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsNull()
        {
            // Arrange
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.GetUser("user");

            // Assert
            Assert.IsNull(result);
        }

        //[TestMethod]
        //public void CheckPermissions()
        //{
        //    // Arrange
        //    var roles = new List<string>() { Security.Role.SystemAdmin };
        //    var service = GetService();
        //    SetupUnitOfWork();

        //    var helper = new PermissionTestHelper(
        //        MockSecurityContext,
        //        roles,
        //        () => {

        //            // Act
        //            var result = service.GetUser(1);

        //            // Assert
        //            Assert.IsNotNull(result);
        //            Assert.IsInstanceOfType(result, typeof(Entities.User));

        //        },
        //        () => {

        //            // Act
        //            var result = service.GetUser(1);

        //            // Assert
        //            Assert.IsNull(result);

        //        });

        //    // Act
        //    helper.CheckRoles();
        //}
    }
}
