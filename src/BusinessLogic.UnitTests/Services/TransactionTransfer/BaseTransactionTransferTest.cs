using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Services.TransactionTransfer
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseTransactionTransferTest
    {
        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected readonly Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();
        protected readonly Mock<ITransactionTransferValidator> MockTransactionTransferValidator = new Mock<ITransactionTransferValidator>();
        protected readonly Mock<IFundService> MockFundService = new Mock<IFundService>();
        protected readonly Mock<IVatService> MockVatService = new Mock<IVatService>();
        protected TransferService GetService()
        {
            var service = new TransferService(
                MockLogger.Object,
                MockUnitOfWork.Object,
                MockSecurityContext.Object,
                MockFundService.Object,
                MockVatService.Object,
                MockTransactionTransferValidator.Object);

            return service;
        }
    }
}
