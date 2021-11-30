using BusinessLogic.Classes.Smtp.Emails;
using BusinessLogic.Entities;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessLogic.UnitTests.Classes.Smtp
{
    [TestClass]
    public class NotificationEmailTests
    {
        private const string emailFrom = "test@test.com";

        private NotificationEmail GetObject(TransactionNotification notification)
        {
            return new NotificationEmail(notification);
        }

        [TestMethod]
        public void PSP_ReferenceIsInEmailBody()
        {
            var notification = new TransactionNotification()
            {
                EventCode = "CHARGEBACK",
                EventDate = new DateTime(2018, 05, 01),
                MerchantReference = "MER001",
                PspReference = "PSP001",
                OriginalReference = "ORI001",
                Reason = "Dispute"
            };
            NotificationEmail emailController = GetObject(notification);

            emailController.Email.Body.Should().Contain(notification.EventCode);
            emailController.Email.Body.Should().Contain(notification.MerchantReference);
            emailController.Email.Body.Should().Contain(notification.PspReference);
            emailController.Email.Body.Should().Contain(notification.OriginalReference);
            emailController.Email.Body.Should().Contain(notification.Reason);

        }
    }
}
