using Admin.Classes.Commands.Import;
using Admin.Interfaces.Commands;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using DependenciesClass = Admin.Controllers.ImportControllerDependencies;

namespace Admin.UnitTests.Classes.Dependencies
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ImportControllerDependenciesTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();

        private readonly Mock<IModelCommand<SaveImportFileCommandArgs>> _mockSaveImportFileCommand = new Mock<IModelCommand<SaveImportFileCommandArgs>>();
        private readonly Mock<IModelCommand<string>> _mockProcessImportCommand = new Mock<IModelCommand<string>>();

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenSaveImportFileCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null,
                    _mockProcessImportCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenSaveImportFileCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null,
                    _mockProcessImportCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: saveImportFileCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenProcessImportCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockSaveImportFileCommand.Object,
                    null);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenProcessImportCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockSaveImportFileCommand.Object,
                    null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: processImportCommand");
            }
        }
    }
}
