using BusinessLogic.Entities;
using BusinessLogic.UnitTests.Helpers;
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
    public class GetTransactionTests : BaseTransactionTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Transactions.Find(
                It.IsAny<Expression<Func<ProcessedTransaction, bool>>>()))
                .Returns(new List<ProcessedTransaction>()
                    {
                        new ProcessedTransaction()
                        {
                            AccountReference = "TransactionReference"
                        }
                    }
                 );

            MockUnitOfWork.Setup(x => x.Transactions.GetByTransactionReference(
                It.IsAny<string>()))
                .Returns(new ProcessedTransaction()
                {
                    AccountReference = "TransactionReference"
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
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetTransaction("TransactionReference");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ProcessedTransaction));
        }

        [TestMethod]
        public void OnErrorReturnsNull()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.GetTransaction("TransactionReference");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsNull()
        {
            // Arrange
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.GetTransaction("TransactionReference");

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void CheckPermissions()
        {
            // Arrange
            var roles = new List<string>() { BusinessLogic.Security.Role.TransactionDetails };
            var service = GetService();
            SetupUnitOfWork();

            var helper = new PermissionTestHelper(
                MockSecurityContext,
                roles,
                () =>
                {

                    // Act
                    var result = service.GetTransaction("TransactionReference");

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(ProcessedTransaction));

                },
                () =>
                {

                    // Act
                    var result = service.GetTransaction("TransactionReference");

                    // Assert
                    Assert.IsNull(result);

                });

            // Act
            helper.CheckRoles();
        }
    }
}
