using BusinessLogic.Entities;
using BusinessLogic.Interfaces.Result;
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
    public class AuthoriseRefundByNotificationTests : BaseTransactionTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.PendingTransactions.Find(
                It.IsAny<Expression<Func<PendingTransaction, bool>>>()))
                .Returns(new List<PendingTransaction>()
                    {
                        {
                            new PendingTransaction()
                            {
                                AccountReference = "Reference"
                            }
                        }
                    });

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

            MockUnitOfWork.Setup(x => x.Transactions.Add(It.IsAny<ProcessedTransaction>()));
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
            var result = service.AuthoriseRefundByNotification("", "");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
        }

        [TestMethod]
        public void ReturnsSuccess()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.AuthoriseRefundByNotification("TEST", "");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsTrue(result.Success);
        }

        [TestMethod]
        public void OnErrorReturnsResult()
        {
            // Arrange
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.AuthoriseRefundByNotification("", "");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void IncorrectSecurityReturnsResult()
        {
            // Arrange
            SetupSecurityContext(false);
            var service = GetService();

            // Act
            var result = service.AuthoriseRefundByNotification("", "");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(IResult));
            Assert.IsFalse(result.Success);
        }

        [TestMethod]
        public void CheckPermissions()
        {
            // Arrange
            var roles = new List<string>() { BusinessLogic.Security.Role.RefundsAuthorise };
            var service = GetService();
            SetupUnitOfWork();

            var helper = new PermissionTestHelper(
                MockSecurityContext,
                roles,
                () =>
                {

                    // Act
                    var result = service.AuthoriseRefundByNotification("TEST", "");

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(IResult));
                    Assert.IsTrue(result.Success);

                },
                () =>
                {

                    // Act
                    var result = service.AuthoriseRefundByNotification("", "");

                    // Assert
                    Assert.IsNotNull(result);
                    Assert.IsInstanceOfType(result, typeof(IResult));
                    Assert.IsFalse(result.Success);

                });

            // Act
            helper.CheckRoles();
        }
    }
}
