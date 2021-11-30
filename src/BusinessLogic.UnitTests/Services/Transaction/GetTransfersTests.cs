using BusinessLogic.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace BusinessLogic.UnitTests.Services.Transaction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetTransfersTests : BaseTransactionTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Transactions.GetTransfers(
                It.IsAny<string>()))
                .Returns(new List<ProcessedTransaction>()
                    {
                        new ProcessedTransaction()
                        {
                            AccountReference = "TransferGuid"
                        }
                    }
                 );
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
            var service = GetService();

            // Act
            var result = service.GetTransfers("TransferGuid");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ProcessedTransaction>));
        }

        [TestMethod]
        public void OnErrorReturnsEmptyList()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.GetTransfers("TransferGuid");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ProcessedTransaction>));
            Assert.AreEqual(result.Any(), false);
        }

        [TestMethod]
        public void NullTransactionReferenceReturnsEmptyList()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.GetTransfers(null);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<ProcessedTransaction>));
            Assert.AreEqual(result.Any(), false);
        }
    }
}
