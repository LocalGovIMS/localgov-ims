using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Strategies;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Service = BusinessLogic.Services.TransactionService;

namespace BusinessLogic.UnitTests.Services.Transaction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseTransactionTest
    {
        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected readonly Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();
        protected readonly Mock<ITransactionVatStrategy> MockVatStrategy = new Mock<ITransactionVatStrategy>();
        protected readonly Mock<IEmailService> MockEmailService = new Mock<IEmailService>();
        protected readonly Mock<IUserService> MockUserService = new Mock<IUserService>();

        protected Service GetService()
        {
            var service = new Service(
                MockLogger.Object,
                MockUnitOfWork.Object,
                MockVatStrategy.Object,
                MockEmailService.Object,
                MockSecurityContext.Object,
                MockUserService.Object);

            return service;
        }
    }
}
