using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Services.Cryptography;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Service = BusinessLogic.Services.PaymentService;

namespace BusinessLogic.UnitTests.Services.Payment
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BasePaymentTest
    {
        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected readonly Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();
        protected readonly Mock<ITransactionService> MockTransactionService = new Mock<ITransactionService>();
        protected readonly Mock<ICryptographyService> MockCryptographyService = new Mock<ICryptographyService>();
        protected readonly Mock<IFundService> MockFundService = new Mock<IFundService>();

        protected Service GetService()
        {
            var service = new Service(
                MockLogger.Object,
                MockUnitOfWork.Object,
                MockSecurityContext.Object,
                MockTransactionService.Object,
                MockCryptographyService.Object,
                MockFundService.Object);

            return service;
        }

    }
}
