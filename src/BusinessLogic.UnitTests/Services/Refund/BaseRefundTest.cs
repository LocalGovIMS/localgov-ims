using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Services.Cryptography;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Service = BusinessLogic.Services.RefundService;

namespace BusinessLogic.UnitTests.Services.Refund
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseRefundTest
    {
        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected readonly Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();
        protected readonly Mock<ITransactionService> MockTransactionService = new Mock<ITransactionService>();
        protected readonly Mock<Clients.PaymentIntegrationClient.IClient> MockPaymentIntegrationClient = new Mock<Clients.PaymentIntegrationClient.IClient>();
        protected readonly Mock<ICryptographyService> MockCryptographyService = new Mock<ICryptographyService>();
        protected readonly Mock<IFundService> MockFundService = new Mock<IFundService>();

        protected Service GetService()
        {
            var service = new Service(
                MockLogger.Object,
                MockUnitOfWork.Object,
                MockSecurityContext.Object,
                MockTransactionService.Object,
                MockPaymentIntegrationClient.Object);

            return service;
        }

    }
}
