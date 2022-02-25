using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Type = BusinessLogic.Entities.PaymentIntegration;

namespace BusinessLogic.UnitTests.Services.PaymentIntegration
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetAllTests : BaseTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.PaymentIntegrations.GetAll())
                .Returns(new List<Type>());
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
            var result = service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Type>));
        }

        [TestMethod]
        public void OnErrorReturnsEmptyList()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<Type>));
            Assert.AreEqual(result.Count, 0);
        }
    }
}
