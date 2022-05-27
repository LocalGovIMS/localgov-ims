
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Service = BusinessLogic.Services.FileImportService;

namespace BusinessLogic.UnitTests.Services.FileImport
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseImportTest
    {
        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected readonly Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();
        protected readonly Mock<IFileImporter> MockFileImporter = new Mock<IFileImporter>();
        protected readonly Mock<IFileImportProcessor> MockFileImportProcessor = new Mock<IFileImportProcessor>();
              
        protected Service GetService()
        {
            var service = new Service(
                MockLogger.Object,
                MockUnitOfWork.Object,
                MockSecurityContext.Object,
                MockFileImporter.Object,
                MockFileImportProcessor.Object);

            return service;
        }
    }
}
