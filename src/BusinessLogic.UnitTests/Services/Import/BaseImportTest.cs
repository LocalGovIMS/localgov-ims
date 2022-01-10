
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Service = BusinessLogic.Services.ImportService;

namespace BusinessLogic.UnitTests.Services.Import
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseImportTest
    {
        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected readonly Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();
        protected readonly Mock<IFileImporter> MockFileImporter = new Mock<IFileImporter>();
        protected readonly Mock<IImportProcessor> MockImportProcessor = new Mock<IImportProcessor>();
              
        protected Service GetService()
        {
            var service = new Service(
                MockLogger.Object,
                MockUnitOfWork.Object,
                MockSecurityContext.Object,
                MockFileImporter.Object,
                MockImportProcessor.Object);

            return service;
        }
    }
}
