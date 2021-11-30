using BusinessLogic.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace BusinessLogic.UnitTests.Services.Transaction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetAmountForPendingTransactionByReferenceTests : BaseTransactionTest
    {
        private void SetupUnitOfWork(bool processedTransactionsExist,
            bool pendingTransactionsExist)
        {
            MockUnitOfWork.Setup(x => x.Transactions.GetByAppReference(
                It.IsAny<string>()))
                .Returns(new ProcessedTransaction()
                {
                    AccountReference = "Reference"
                }
            );

            if (processedTransactionsExist)
            {
                MockUnitOfWork.Setup(x => x.Transactions.Find(
                    It.IsAny<Expression<Func<ProcessedTransaction, bool>>>()))
                    .Returns(new List<ProcessedTransaction>()
                        {
                        new ProcessedTransaction()
                        {
                            AccountReference = "Reference"
                        }
                        }
                     );
            }

            if (pendingTransactionsExist)
            {
                MockUnitOfWork.Setup(x => x.PendingTransactions.GetByInternalReference(
                    It.IsAny<string>()))
                    .Returns(new List<PendingTransaction>()
                    {
                        {
                            new PendingTransaction()
                            {
                                AccountReference = "Reference1",
                                Amount = 1
                            }
                        },
                        {
                            new PendingTransaction()
                            {
                                AccountReference = "Reference2",
                                Amount = 1.5M
                            }
                        }
                    });
            }
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
            SetupUnitOfWork(true, true);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetAmountForPendingTransactionByReference("Reference");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(decimal));
        }

        [TestMethod]
        public void ReturnsCorrectValue()
        {
            // Arrange
            SetupUnitOfWork(false, true);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetAmountForPendingTransactionByReference("Reference");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(decimal));
            Assert.AreEqual(result, 2.5M);
        }

        [TestMethod]
        public void IfProcessedTransactionsExistReturnsZero()
        {
            // Arrange
            SetupUnitOfWork(true, true);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetAmountForPendingTransactionByReference("Reference");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(decimal));
            Assert.AreEqual(result, decimal.Zero);
        }
    }
}
