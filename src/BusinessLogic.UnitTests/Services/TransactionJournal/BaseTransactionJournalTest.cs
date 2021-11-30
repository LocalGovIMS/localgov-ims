using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Strategies;
using BusinessLogic.Interfaces.Validators;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Service = BusinessLogic.Services.TransactionJournalService;

namespace BusinessLogic.UnitTests.Services.TransactionJournal
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseTransactionJournalTest
    {
        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected readonly Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();

        protected readonly Mock<ITransactionJournalValidator> MockTransactionJournalValidator = new Mock<ITransactionJournalValidator>();
        protected readonly Mock<IRollbackTransactionJournalValidator> MockRollbackTransactionJournalValidator = new Mock<IRollbackTransactionJournalValidator>();
        protected readonly Mock<ITransactionService> MockTransactionService = new Mock<ITransactionService>();
        protected readonly Mock<ITransactionVatStrategy> MockTransactionVatStrategy = new Mock<ITransactionVatStrategy>();

        protected Service GetService()
        {
            var service = new Service(
                MockLogger.Object,
                MockUnitOfWork.Object,
                MockSecurityContext.Object,
                MockTransactionJournalValidator.Object,
                MockRollbackTransactionJournalValidator.Object,
                MockTransactionService.Object,
                MockTransactionVatStrategy.Object);

            return service;
        }
    }
}
