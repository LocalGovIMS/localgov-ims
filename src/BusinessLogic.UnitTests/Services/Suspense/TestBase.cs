using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Strategies;
using log4net;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Service = BusinessLogic.Services.SuspenseService;

namespace BusinessLogic.UnitTests.Services.Suspense
{
    [ExcludeFromCodeCoverage]
    public abstract class TestBase
    {
        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected readonly Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();
        protected readonly Mock<IJournalAllocationStrategy> MockJournalAllocationStrategy = new Mock<IJournalAllocationStrategy>();

        protected Service GetService()
        {
            var service = new Service(
                MockLogger.Object,
                MockUnitOfWork.Object,
                MockSecurityContext.Object,
                MockJournalAllocationStrategy.Object);

            return service;
        }
    }
}
