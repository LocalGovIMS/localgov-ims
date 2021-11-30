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
