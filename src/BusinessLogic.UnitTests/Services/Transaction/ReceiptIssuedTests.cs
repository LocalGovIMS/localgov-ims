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
    public class ReceiptIssuedTests : BaseTransactionTest
    {
        private const string CardViaStaffMopCode = "92";

        private void SetupUnitOfWork(bool returnTransactions, string transactionMopCode)
        {
            MockUnitOfWork.Setup(x => x.Transactions.GetByPspReference(
                It.IsAny<string>()))
                .Returns(returnTransactions
                    ? new List<ProcessedTransaction>()
                        {
                            new ProcessedTransaction()
                            {
                                MopCode = transactionMopCode,
                                Mop = new Mop()
                                {
                                    MopCode = transactionMopCode
                                }
                            }
                        }
                    : new List<ProcessedTransaction>()
                 );

            MockUnitOfWork.Setup(x => x.PendingTransactions.GetPendingRefunds(
                It.IsAny<string>()))
                .Returns(new List<PendingTransaction>()
                    {
                        new PendingTransaction()
                        {
                            AccountReference = "TransactionReference"
                        }
                    }
                 );

            MockUnitOfWork.Setup(x => x.Transactions.GetProcessedRefunds(
            It.IsAny<string>()))
            .Returns(new List<ProcessedTransaction>()
                {
                                new ProcessedTransaction()
                                {
                                    AccountReference = "TransactionReference"
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
            SetupUnitOfWork(true, CardViaStaffMopCode);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.ReceiptIssued("PspReference");

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
            SetupSecurityContext(true);

            // Act
            var result = service.ReceiptIssued("PspReference");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsFalse(result.Success);
        }
    }
}
