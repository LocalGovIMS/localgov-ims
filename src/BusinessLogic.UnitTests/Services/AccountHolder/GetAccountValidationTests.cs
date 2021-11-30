using BusinessLogic.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.AccountHolder
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetAccountValidationTests : BaseAccountHolderTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.AccountValidations.GetAccountValidation(It.IsAny<string>()))
                .Returns(new AccountValidation());
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.GetAccountValidation("Test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(AccountValidation));
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.GetAccountValidation("Test");

            // Assert
            Assert.IsNull(result);
        }

    }
}
