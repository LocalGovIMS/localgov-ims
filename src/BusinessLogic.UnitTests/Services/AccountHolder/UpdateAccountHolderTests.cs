using BusinessLogic.Classes.Result;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.AccountHolder
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UpdateAccountHolderTests : BaseAccountHolderTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.AccountHolders.GetByAccountReference(It.IsAny<string>()))
                .Returns(new Entities.AccountHolder());
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
            var result = service.Update(new Entities.AccountHolder());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
        }

        [TestMethod]
        public void UpdateReturnsSuccess()
        {
            // Arrange
            SetupUnitOfWork();
            SetupAccountHolderFundMessageValidator(new Result());

            var service = GetService();

            // Act
            var result = service.Update(new Entities.AccountHolder());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, true);
        }

        [TestMethod]
        public void UpdateErrorReturnsFailure()
        {
            // Arrange
            SetupAccountHolderFundMessageValidator(new Result());

            var service = GetService();

            // Act
            var result = service.Update(new Entities.AccountHolder());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public void UpdateToAnAccountHolderThatDoesNotExistReturnsFailure()
        {
            // Arrange
            SetupAccountHolderFundMessageValidator(new Result());
            SetupUnitOfWork();

            var service = GetService();

            MockUnitOfWork.Setup(x => x.AccountHolders.GetByAccountReference(It.IsAny<string>()))
               .Returns((Entities.AccountHolder)null);

            // Act
            var result = service.Update(new Entities.AccountHolder());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, false);
            Assert.AreEqual(result.Error, "Unable to find the Account Holder record to update");
        }

        [TestMethod]
        public void UpdateReturnsErrorIfAccountHolderFundMessageValidationFails()
        {
            // Arrange
            SetupAccountHolderFundMessageValidator(new Result("error"));
            SetupUnitOfWork();

            var service = GetService();

            // Act
            var result = service.Update(new Entities.AccountHolder());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public void UnexpectedExceptionReturnsError()
        {
            // Arrange
            MockAccountHolderFundMessageValidator.Setup(x => x.Validate(It.IsAny<Entities.AccountHolder>()))
                .Throws(new InvalidOperationException());

            var service = GetService();

            // Act
            var result = service.Update(new Entities.AccountHolder());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Result));
            Assert.AreEqual(result.Success, false);
            Assert.AreEqual(result.Error, "Unable to update Account Holder");
        }
    }
}
