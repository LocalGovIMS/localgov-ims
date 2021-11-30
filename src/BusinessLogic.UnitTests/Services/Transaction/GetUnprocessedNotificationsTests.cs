using BusinessLogic.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;

namespace BusinessLogic.UnitTests.Services.Transaction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class GetUnprocessedNotificationsTests : BaseTransactionTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.TransactionNotifications.Find(
                It.IsAny<Expression<Func<TransactionNotification, bool>>>()))
                .Returns(new List<TransactionNotification>()
                    {
                        new TransactionNotification()
                        {
                            PspReference = "PspReference"
                        }
                    }
                 );
        }

        [TestMethod]
        public void ReturnsCorrectType()
        {
            // Arrange
            SetupUnitOfWork();
            var service = GetService();

            // Act
            var result = service.GetUnprocessedNotifications();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<TransactionNotification>));
        }

        [TestMethod]
        public void OnErrorReturnsEmptyList()
        {
            // Arrange
            var service = GetService();

            // Act
            var result = service.GetUnprocessedNotifications();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<TransactionNotification>));
            Assert.AreEqual(result.Any(), false);
        }
    }
}
