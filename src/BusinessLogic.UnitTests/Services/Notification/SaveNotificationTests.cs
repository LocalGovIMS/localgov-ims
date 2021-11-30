using BusinessLogic.Classes;
using BusinessLogic.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.Notification
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SaveNotificationTests : BaseNotificationTest
    {
        private void SetupUnitOfWork()
        {
            MockUnitOfWork.Setup(x => x.Roles.GetAll())
                .Returns(new List<Entities.Role>());
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
            var result = service.SaveNotification(new TransactionNotification());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(bool));
        }

        [TestMethod]
        public void ReturnsFalseOnError()
        {
            // Arrange
            MockUnitOfWork.Setup(x => x.Complete(It.IsAny<int>())).Throws(new Exception("Error"));
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.SaveNotification(new TransactionNotification());

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(false, result);
        }

        [TestMethod]
        public void RemovesSensitiveDataBeforeSave()
        {
            // Arrange
            MockUnitOfWork.Setup(x => x.Complete(It.IsAny<int>()));
            MockUnitOfWork.Setup(x => x.TransactionNotifications.Add(It.IsAny<TransactionNotification>()));
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.SaveNotification(new TransactionNotification()
            {
                EventCode = NotificationEvent.Authorisation,
                Reason = "123456:LAST4CARD:EXPIRY"
            });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);
            MockUnitOfWork.Verify(x => x.TransactionNotifications.Add(It.Is<TransactionNotification>(y => y.Reason == "123456")));
        }

        [TestMethod]
        public void SaveReasonIfNoSensitiveDataDetected()
        {
            // Arrange
            MockUnitOfWork.Setup(x => x.Complete(It.IsAny<int>()));
            MockUnitOfWork.Setup(x => x.TransactionNotifications.Add(It.IsAny<TransactionNotification>()));
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            var result = service.SaveNotification(new TransactionNotification()
            {
                EventCode = NotificationEvent.Authorisation,
                Reason = "123456"
            });

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(true, result);
            MockUnitOfWork.Verify(x => x.TransactionNotifications.Add(It.Is<TransactionNotification>(y => y.Reason == "123456")));
        }
    }
}
