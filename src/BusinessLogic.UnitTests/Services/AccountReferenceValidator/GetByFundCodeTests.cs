using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.AccountReferenceValidator
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetByFundCodeTests : BaseAccountReferenceValidatorTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.AccountReferenceValidators.GetByFundCode(
                It.IsAny<string>()))
                .Returns(new Entities.AccountReferenceValidator());
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
            var result = service.GetByFundCode("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Entities.AccountReferenceValidator));
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetByFundCode("Test");

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
            var result = service.GetByFundCode("Test");

            // Assert
            Assert.IsNull(result);
        }
    }
}
