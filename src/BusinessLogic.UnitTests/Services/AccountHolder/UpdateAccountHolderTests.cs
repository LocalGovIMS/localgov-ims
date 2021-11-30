using BusinessLogic.Classes.Result;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
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
    }
}
