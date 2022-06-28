using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace BusinessLogic.UnitTests.Services.TransactionImport
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetTests : BaseTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.TransactionImports.Get(It.IsAny<int>()))
                .Returns(new Entities.TransactionImport());
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.Get(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Entities.TransactionImport));
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.Get(1);

            // Assert
            Assert.IsNull(result);
        }
    }
}
