using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.UserMethodOfPayment
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetByUserIdTests : BaseUserMethodOfPaymentTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.UserMethodOfPayments.GetByUserId(It.IsAny<int>()))
                .Returns(new List<Entities.UserMethodOfPayment>() {
                    new Entities.UserMethodOfPayment()
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
            var result = service.GetByUserId(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Entities.UserMethodOfPayment>));
        }

        [TestMethod]
        public void OnErrorReturnsEmptyList()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetByUserId(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Entities.UserMethodOfPayment>));
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsEmptyList()
        {
            // Arrange
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.GetByUserId(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Entities.UserMethodOfPayment>));
            Assert.AreEqual(result.Count, 0);
        }

        [TestMethod]
        public void CheckPermissions()
        {
            // Arrange
            var roles = new List<string>() { BusinessLogic.Security.Role.PostPayment, BusinessLogic.Security.Role.SystemAdmin };
            var service = GetService();
            SetupUnitOfWork();

            var helper = new PermissionTestHelper(
                MockSecurityContext,
                roles,
                () =>
                {
                    // Act
                    var result = service.GetByUserId(1);

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(List<Entities.UserMethodOfPayment>));
                },
                () =>
                {
                    // Act
                    var result = service.GetByUserId(1);

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(List<Entities.UserMethodOfPayment>));
                    Assert.AreEqual(result.Count, 0);
                });

            // Act
            helper.CheckRoles();
        }
    }
}
