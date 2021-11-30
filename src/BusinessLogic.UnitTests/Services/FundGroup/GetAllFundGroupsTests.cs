using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.FundGroup
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetAllFundGroupsTests : BaseFundGroupTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.FundGroups.GetAll())
                .Returns(new List<Entities.FundGroup>());
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
            var result = service.GetAllFundGroups();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Entities.FundGroup>));
        }

        [TestMethod]
        public void OnErrorReturnsEmptyList()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetAllFundGroups();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Entities.FundGroup>));
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsEmptyList()
        {
            // Arrange
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.GetAllFundGroups();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Entities.FundGroup>));
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
                    var result = service.GetAllFundGroups();

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(List<Entities.FundGroup>));

                },
                () =>
                {

                    // Act
                    var result = service.GetAllFundGroups();

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(List<Entities.FundGroup>));
                    Assert.AreEqual(result.Count, 0);

                });

            // Act
            helper.CheckRoles();
        }
    }
}
