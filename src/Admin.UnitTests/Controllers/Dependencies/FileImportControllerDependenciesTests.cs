using Admin.Classes.Commands.FileImport;
using Admin.Interfaces.Commands;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using DependenciesClass = Admin.Controllers.FileImportControllerDependencies;

namespace Admin.UnitTests.Classes.Dependencies
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FileImportControllerDependenciesTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();

        private readonly Mock<IModelCommand<SaveCommandArgs>> _mockSaveCommand = new Mock<IModelCommand<SaveCommandArgs>>();
        private readonly Mock<IModelCommand<int>> _mockProcessCommand = new Mock<IModelCommand<int>>();

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenSaveCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null,
                    _mockProcessCommand.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenSaveCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null,
                    _mockProcessCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: saveCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenProcessCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockSaveCommand.Object,
                    null);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenProcessCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockSaveCommand.Object,
                    null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: processCommand");
            }
        }
    }
}
