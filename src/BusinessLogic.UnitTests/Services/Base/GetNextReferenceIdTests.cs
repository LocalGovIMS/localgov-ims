using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BusinessLogic.UnitTests.Services
{
    [TestClass]
    public class GetNextReferenceIdTests
    {
        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected readonly Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();

        protected TestBaseService GetService()
        {
            var service = new TestBaseService(
                MockLogger.Object,
                MockUnitOfWork.Object,
                MockSecurityContext.Object);

            return service;
        }

        //  HIGH - not deterministic so needs replacing but has some value for now.
        [TestMethod]
        public void TestForCollisionForMultipleIDGeneration()
        {
            // Arrange
            var baseService = GetService();
            var generatedReferences = new List<string>();

            // Act
            for (var i = 0; i <= 100; i++)
            {
                Thread.Sleep(1);
                generatedReferences.Add(baseService.NextReferenceId());
            }

            // Assert
            Assert.AreEqual(false, generatedReferences.GroupBy(n => n).Any(c => c.Count() > 1));
        }
    }

    public class TestBaseService : BaseService
    {
        public TestBaseService(ILog logger,
            IUnitOfWork unitOfWork,
            ISecurityContext securityContext
            ) : base(logger, unitOfWork, securityContext)
        {
        }

        public string NextReferenceId()
        {
            return base.GetNextReferenceId();
        }
    }
}
