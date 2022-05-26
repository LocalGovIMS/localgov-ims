using BusinessLogic.Classes.Result;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.AccountHolder
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateAccountHolderTests : BaseAccountHolderTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.AccountHolders.Add(It.IsAny<Entities.AccountHolder>()));
        }

        private void SetupAccountHolderFundMessageValidator(Result result)
        {
            MockAccountHolderFundMessageValidator.Setup(x => x.Validate(It.IsAny<Entities.AccountHolder>()))
                .Returns(result);
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.Create(new Entities.AccountHolder());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
        }

        [TestMethod]
        public void CreateReturnsSuccess()
        {
            // Arrange
            SetupUnitOfWork();
            SetupAccountHolderFundMessageValidator(new Result());

            var service = GetService();

            // Act
            var result = service.Create(new Entities.AccountHolder());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, true);
        }

        [TestMethod]
        public void CreateErrorReturnsFailure()
        {
            // Arrange
            SetupAccountHolderFundMessageValidator(new Result());

            var service = GetService();

            // Act
            var result = service.Create(new Entities.AccountHolder());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public void CreateReturnsErrorIfAccountHolderFundMessageValidationFails()
        {
            // Arrange
            SetupAccountHolderFundMessageValidator(new Result("error"));
            SetupUnitOfWork();

            var service = GetService();

            // Act
            var result = service.Create(new Entities.AccountHolder());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, false);
        }
    }
}
