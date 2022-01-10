using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessLogic.UnitTests.ImportProcessing.FileImporter
{
    public class ConstructorTests : TestBase
    {
        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenLoggerIsNull()
        {
            try
            {
                var fileImport = new BusinessLogic.ImportProcessing.FileImporter(
                    null,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockFileSystem.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionMessageWhenLoggerIsNull()
        {
            try
            {
                var fileImport = new BusinessLogic.ImportProcessing.FileImporter(
                    null,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockFileSystem.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: log");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenSecurityContextIsNull()
        {
            try
            {
                var fileImport = new BusinessLogic.ImportProcessing.FileImporter(
                    MockLogger.Object,
                    null,
                    MockUnitOfWork.Object,
                    MockFileSystem.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionMessageWhenSecurityContextIsNull()
        {
            try
            {
                var fileImport = new BusinessLogic.ImportProcessing.FileImporter(
                    MockLogger.Object,
                    null,
                    MockUnitOfWork.Object,
                    MockFileSystem.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: securityContext");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenUnitOfWorkIsNull()
        {
            try
            {
                var fileImport = new BusinessLogic.ImportProcessing.FileImporter(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    null,
                    MockFileSystem.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionMessageWhenUnitOfWorkIsNull()
        {
            try
            {
                var fileImport = new BusinessLogic.ImportProcessing.FileImporter(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    null,
                    MockFileSystem.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: unitOfWork");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenFileSystemIsNull()
        {
            try
            {
                var fileImport = new BusinessLogic.ImportProcessing.FileImporter(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    null);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionMessageWhenFileSystemIsNull()
        {
            try
            {
                var fileImport = new BusinessLogic.ImportProcessing.FileImporter(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: fileSystem");
            }
        }
    }
}
