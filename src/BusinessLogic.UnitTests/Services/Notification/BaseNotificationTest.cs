using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.Notification
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseNotificationTest
    {
        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected readonly Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();
        protected readonly Mock<ITransactionService> MockTransactionService = new Mock<ITransactionService>();
        protected readonly Mock<IEmailService> MockEmailService = new Mock<IEmailService>();

        protected NotificationService GetService()
        {
            var service = new NotificationService(
                MockLogger.Object,
                MockUnitOfWork.Object,
                MockSecurityContext.Object,
                MockTransactionService.Object,
                MockEmailService.Object);

            return service;
        }
    }
}
