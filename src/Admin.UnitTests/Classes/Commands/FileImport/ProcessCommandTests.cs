using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.FileImport.ProcessCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;

namespace Admin.UnitTests.Classes.Commands.FileImport
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ProcessCommandTests
    {
        private const int TransactionImportId = 1;

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFileImportService> _mockFileImportService = new Mock<IFileImportService>();

        public ProcessCommandTests()
        {
            _mockFileImportService.Setup(x => x.Process(It.IsAny<int>()))
                .Returns(new Result() { Data = new ProcessResult() });

        }

        private Command CrateCommand()
        {
            return new Command(
                _mockLogger.Object,
                _mockFileImportService.Object);
        }

        [TestMethod]
        public void Execute_returns_a_CommandResult()
        {
            // Arrange
            var command = CrateCommand();

            // Act
            var result = command.Execute(TransactionImportId);

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
            var result = command.Execute(TransactionImportId);

            // Assert
            result.Messages[0].Should().Be("File processed successfully.");
        }

        [TestMethod]
        public void FileImportService_Process_method_is_called_once()
        {
            // Arrange
            var command = CrateCommand();

            // Act
            var result = command.Execute(TransactionImportId);

            // Assert
            _mockFileImportService.Verify(x => x.Process(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void When_FileImportService_LoadFromFile_fails_the_error_message_is_returned()
        {
            // Arrange
            var command = CrateCommand();
            
            var expectedErrorMessage = "An error message";

            _mockFileImportService.Setup(x => x.Process(It.IsAny<int>()))
                .Returns(new Result(expectedErrorMessage) { Data = new ProcessResult() });

            // Act
            var result = command.Execute(TransactionImportId);

            // Assert
            result.Messages[0].Should().Be(expectedErrorMessage);
        }
    }
}
