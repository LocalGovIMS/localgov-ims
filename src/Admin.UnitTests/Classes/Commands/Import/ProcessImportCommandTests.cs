using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.Import.ProcessImportCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;

namespace Admin.UnitTests.Classes.Commands.Import
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ProcessImportCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportService> _mockImportService = new Mock<IImportService>();

        public ProcessImportCommandTests()
        {
            _mockImportService.Setup(x => x.Process(It.IsAny<string>()))
                .Returns(new Result() { Data = new ProcessResult() });

        }

        private Command CrateCommand()
        {
            return new Command(
                _mockLogger.Object,
                _mockImportService.Object);
        }

        [TestMethod]
        public void Execute_returns_a_CommandResult()
        {
            // Arrange
            var command = CrateCommand();

            // Act
            var result = command.Execute("Batch Reference");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void Execute_returns_a_success_message_when_all_is_ok()
        {
            // Arrange
            var command = CrateCommand();

            // Act
            var result = command.Execute("Batch Reference");

            // Assert
            result.Messages[0].Should().Be("File processed successfully.");
        }

        [TestMethod]
        public void ImportService_Process_method_is_called_once()
        {
            // Arrange
            var command = CrateCommand();

            // Act
            var result = command.Execute("Batch Reference");

            // Assert
            _mockImportService.Verify(x => x.Process(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void When_ImportService_LoadFromFile_fails_the_error_message_is_returned()
        {
            // Arrange
            var command = CrateCommand();
            
            var expectedErrorMessage = "An error message";

            _mockImportService.Setup(x => x.Process(It.IsAny<string>()))
                .Returns(new Result(expectedErrorMessage) { Data = new ProcessResult() });

            // Act
            var result = command.Execute("Batch Reference");

            // Assert
            result.Messages[0].Should().Be(expectedErrorMessage);
        }
    }
}
