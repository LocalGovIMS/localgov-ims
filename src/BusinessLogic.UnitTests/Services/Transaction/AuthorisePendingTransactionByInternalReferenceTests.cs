using BusinessLogic.Entities;
using BusinessLogic.Models;
using BusinessLogic.Models.Transactions;
using BusinessLogic.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.Transaction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class AuthorisePendingTransactionByInternalReferenceTests : BaseTransactionTest
    {
        private void SetupUnitOfWork(bool transactionProcessed)
        {
            MockUnitOfWork.Setup(x => x.Transactions.GetByInternalReference(
                It.IsAny<string>()))
                .Returns(transactionProcessed
                    ? new List<ProcessedTransaction>()
                    {
                        {
                            new ProcessedTransaction()
                            {
                                AccountReference = "Reference"
                            }
                        }
                    }
                    : new List<ProcessedTransaction>());

            MockUnitOfWork.Setup(x => x.PendingTransactions.GetByInternalReference(
                It.IsAny<string>()))
                .Returns(new List<PendingTransaction>()
                {
                    {
                        new PendingTransaction()
                        {
                            AccountReference = "Reference"
                        }
                    }
                });
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
            SetupUnitOfWork(false);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.AuthorisePendingTransactionByInternalReference(new AuthorisePendingTransactionByInternalReferenceArgs());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Response));
        }

        [TestMethod]
        public void ReturnsSuccess()
        {
            // Arrange
            SetupUnitOfWork(false);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.AuthorisePendingTransactionByInternalReference(new AuthorisePendingTransactionByInternalReferenceArgs());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Response));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void OnErrorReturnsResponseFail()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.AuthorisePendingTransactionByInternalReference(new AuthorisePendingTransactionByInternalReferenceArgs());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Response));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsNull()
        {
            // Arrange
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.AuthorisePendingTransactionByInternalReference(new AuthorisePendingTransactionByInternalReferenceArgs());

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void TransactionAlreadyProcessedReturnsResponse()
        {
            // Arrange
            SetupUnitOfWork(true);
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.AuthorisePendingTransactionByInternalReference(new AuthorisePendingTransactionByInternalReferenceArgs());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(Response));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void CheckPermissions()
        {
            // Arrange
            var roles = new List<string>() { BusinessLogic.Security.Role.TransactionCreate };
            var service = GetService();
            SetupUnitOfWork(false);

            var helper = new PermissionTestHelper(
                MockSecurityContext,
                roles,
                () =>
                {

                    // Act
                    var result = service.AuthorisePendingTransactionByInternalReference(new AuthorisePendingTransactionByInternalReferenceArgs());

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(Response));
                    Assert.IsTrue(result.Success);

                },
                () =>
                {

                    // Act
                    var result = service.AuthorisePendingTransactionByInternalReference(new AuthorisePendingTransactionByInternalReferenceArgs());

                    // Assert
                    Assert.IsNull(result);

                });

            // Act
            helper.CheckRoles();
        }
    }
}
