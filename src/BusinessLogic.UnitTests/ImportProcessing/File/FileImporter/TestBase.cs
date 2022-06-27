using BusinessLogic.Interfaces.Persistence;
using BusinessLogic.Interfaces.Security;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.IO.Abstractions;

namespace BusinessLogic.UnitTests.ImportProcessing.File.FileImporter
{
    [TestClass]
    public class TestBase
    {
        protected Mock<ILog> MockLogger = new Mock<ILog>();
        protected Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();
        protected Mock<IUnitOfWork> MockUnitOfWork = new Mock<IUnitOfWork>();
        protected Mock<IFileSystem> MockFileSystem = new Mock<IFileSystem>();
    }
}
