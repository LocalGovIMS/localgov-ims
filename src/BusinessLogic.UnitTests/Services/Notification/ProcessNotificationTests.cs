using BusinessLogic.Classes;
using BusinessLogic.Classes.Result;
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
    public class ProcessNotificationTests : BaseNotificationTest
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
        public void CorrectlyMarksProcessedNotifications()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            MockTransactionService.Setup(x => x.GetUnprocessedNotifications()).Returns(
                new List<TransactionNotification>()
                {
                    new TransactionNotification(),
                    new TransactionNotification(),
                    new TransactionNotification()
                });
            var service = GetService();

            // Act
            service.ProcessNotifications();

            // Assert            
            MockTransactionService.Verify(x => x.MarkNotificationAsProcessed(It.IsAny<int>()), Times.Exactly(3));
        }

        [TestMethod]
        public void OnlyMarksProcessedNotificationsAsProcessed()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            MockTransactionService.Setup(x => x.GetUnprocessedNotifications()).Returns(
                new List<TransactionNotification>()
                {
                    new TransactionNotification() { Id=1, EventCode = NotificationEvent.Refund, MerchantReference="A1"},
                    new TransactionNotification() { Id=2, EventCode = NotificationEvent.Refund, MerchantReference="A2"}
                });
            MockTransactionService.Setup(x => x.MarkRefundsAsFailed("A1", It.IsAny<string>()))
                .Throws(new Exception("Error saving"));
            MockTransactionService.Setup(x => x.MarkRefundsAsFailed("A2", It.IsAny<string>()))
                .Returns(new Result());
            var service = GetService();

            // Act
            service.ProcessNotifications();

            // Assert            
            MockTransactionService.Verify(x => x.MarkNotificationAsProcessed(It.IsAny<int>()), Times.Once);
            MockTransactionService.Verify(x => x.MarkNotificationAsProcessed(2), Times.Once);
        }

        [TestMethod]
        public void ProcessesAuthorisationNotification()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            var service = GetService();
            var notification = new TransactionNotification()
            {
                EventCode = NotificationEvent.Authorisation,
                Success = true,
                MerchantReference = "TEST",
                PspReference = "PSP",
                Reason = "AUTHCODE:LAST4:EXPIRY"
            };

            // Act
            service.ProcessNotification(notification);

            // Assert            
            MockTransactionService.Verify(x => x.AuthoriseTransactionByNotification(notification), Times.Once);
        }

        [TestMethod]
        public void ProcessesSuccessfulRefundNotification()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            service.ProcessNotification(new TransactionNotification()
            {
                EventCode = NotificationEvent.Refund,
                Success = true,
                MerchantReference = "TEST",
                PspReference = "PSP",
            });

            // Assert            
            MockTransactionService.Verify(x => x.AuthoriseRefundByNotification("TEST", "PSP"), Times.Once);
        }

        [TestMethod]
        public void ProcessesFailedRefundNotification()
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            service.ProcessNotification(new TransactionNotification()
            {
                EventCode = NotificationEvent.Refund,
                Success = false,
                MerchantReference = "TEST",
                PspReference = "PSP",
                Reason = "Card expired"
            });

            // Assert            
            MockTransactionService.Verify(x => x.MarkRefundsAsFailed("TEST", "Card expired"), Times.Once);
        }

        [DataTestMethod]
        [DataRow(NotificationEvent.Chargeback, 1)]
        [DataRow(NotificationEvent.ChargebackReversed, 1)]
        [DataRow(NotificationEvent.NotificationOfChargeback, 1)]
        [DataRow(NotificationEvent.RequestForInformation, 1)]
        [DataRow(NotificationEvent.Authorisation, 0)]
        [DataRow(NotificationEvent.Refund, 0)]
        [DataRow(NotificationEvent.ReportAvailable, 0)]
        public void ProcessUnexpectedNotificationsAndEmail(string notificationEvent, int expectedEmailsSent)
        {
            // Arrange
            SetupUnitOfWork();
            SetupSecurityContext(true);
            var service = GetService();

            // Act
            service.ProcessNotification(new TransactionNotification()
            {
                EventCode = notificationEvent,
                Success = false,
                MerchantReference = "TEST",
                PspReference = "PSP",
                Reason = "Card expired"
            });

            // Assert            
            MockEmailService.Verify(x => x.SendNotificationEmail(It.IsAny<TransactionNotification>()), Times.Exactly(expectedEmailsSent));
        }

    }
}
