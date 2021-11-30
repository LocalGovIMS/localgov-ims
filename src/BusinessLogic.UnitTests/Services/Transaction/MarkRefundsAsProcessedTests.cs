using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.Transaction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class MarkRefundsAsProcessedTests : BaseTransactionTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.PendingTransactions.GetPendingRefunds(
                It.IsAny<string>()))
                .Returns(new List<PendingTransaction>() {
                    new PendingTransaction()
                    {
                        TransactionReference = "TransactionReference"
                    }
                });
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.MarkRefundsAsFailed("TransactionReference", "test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void OnErrorReturnsFailure()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.MarkRefundsAsFailed("TransactionReference", "test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void WhiteSpaceReferenceReturnsFailure()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.MarkRefundsAsFailed("", "test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsFalse(result.Success);
        }
    }
}
