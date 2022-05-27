using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.IO.Abstractions;
using System.Web;
using Command = Admin.Classes.Commands.FileImport.SaveCommand;
using CommandArgs = Admin.Classes.Commands.FileImport.SaveCommandArgs;
using CommandResult = Admin.Classes.Commands.CommandResult;

namespace Admin.UnitTests.Classes.Commands.FileImport
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SaveCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFileImportService> _mockFileImportService = new Mock<IFileImportService>();
        private readonly Mock<IFileSystem> _mockFileSystem = new Mock<IFileSystem>();
        private readonly Mock<HttpPostedFileBase> _mockFile = new Mock<HttpPostedFileBase>();

        public SaveCommandTests()
        {
            _mockFileSystem.Setup(x => x.Directory.Exists(It.IsAny<string>()))
                .Returns(true);

            _mockFileSystem.Setup(x => x.Path.GetExtension(It.IsAny<string>()))
                .Returns("csv");

            _mockFileImportService.Setup(x => x.LoadFromFile(It.IsAny<string>()))
                .Returns(new Result() { Data = new LoadFromFileResult() });

        }

        private CommandArgs GenerateArgs()
        {
            _mockFile.Setup(x => x.FileName).Returns("import-data.csv");

            return new CommandArgs()
            {
                File = _mockFile.Object,
                Path = "A path"
            };
        }

        private Command CrateCommand()
        {
            return new Command(
                _mockLogger.Object,
                _mockFileImportService.Object,
                _mockFileSystem.Object);
        }

        [TestMethod]
        public void Execute_returns_a_CommandResult()
        {
            // Arrange
            var command = CrateCommand();

            // Act
            var result = command.Execute(GenerateArgs());

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
            var result = command.Execute(GenerateArgs());

            // Assert
            result.Messages[0].Should().Be("File uploaded successfully.");
        }

        [TestMethod]
        public void CommandResult_is_unsuccessful_when_file_is_null()
        {
            // Arrange
            var args = GenerateArgs();
            var command = CrateCommand();
            
            args.File = null;

            // Act
            var result = command.Execute(args);

            // Assert
            result.Success.Should().BeFalse();
        }

        [TestMethod]
        public void CommandResult_returns_expected_error_message_when_file_is_null()
        {
            // Arrange
            var args = GenerateArgs();
            var command = CrateCommand();
            
            args.File = null;

            // Act
            var result = command.Execute(args);

            // Assert
            result.Messages[0].Should().Be("Unable to locate file");
        }

        [TestMethod]
        public void CommandResult_is_unsuccessful_when_path_is_null()
        {
            // Arrange
            var args = GenerateArgs();
            var command = CrateCommand();

            args.Path = null;

            // Act
            var result = command.Execute(args);

            // Assert
            result.Success.Should().BeFalse();
        }

        [TestMethod]
        public void CommandResult_returns_expected_error_message_when_path_is_null()
        {
            // Arrange
            var args = GenerateArgs();
            var command = CrateCommand();

            args.Path = null;

            // Act
            var result = command.Execute(args);

            // Assert
            result.Messages[0].Should().Be("The path to save the file to is not specified");
        }

        [TestMethod]
        public void CreateDirectory_is_called_when_path_does_not_exist()
        {
            // Arrange
            var args = GenerateArgs();
            
            _mockFileSystem.Setup(x => x.Directory.Exists(It.IsAny<string>()))
                .Returns(false);

            var command = CrateCommand();

            // Act
            var result = command.Execute(args);

            // Assert
            _mockFileSystem.Verify(x => x.Directory.CreateDirectory(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void CreateDirectory_is_not_called_when_path_does_exist()
        {
            // Arrange
            var args = GenerateArgs();
            var command = CrateCommand();

            // Act
            var result = command.Execute(args);

            // Assert
            _mockFileSystem.Verify(x => x.Directory.CreateDirectory(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public void File_is_saved()
        {
            // Arrange
            var args = GenerateArgs();
            var command = CrateCommand();

            // Act
            var result = command.Execute(args);

            // Assert
            _mockFile.Verify(x => x.SaveAs(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void FileImportService_LoadFromFile_method_is_called_once()
        {
            // Arrange
            var args = GenerateArgs();
            var command = CrateCommand();

            // Act
            var result = command.Execute(args);

            // Assert
            _mockFileImportService.Verify(x => x.LoadFromFile(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public void When_FileImportService_LoadFromFile_fails_the_error_message_is_returned()
        {
            // Arrange
            var args = GenerateArgs();
            var command = CrateCommand();
            
            var expectedErrorMessage = "An error message";

            _mockFileImportService.Setup(x => x.LoadFromFile(It.IsAny<string>()))
                .Returns(new Result(expectedErrorMessage) { Data = new LoadFromFileResult() });

            // Act
            var result = command.Execute(args);

            // Assert
            result.Messages[0].Should().Be(expectedErrorMessage);
        }
        

        [TestMethod]
        public void When_FileImportService_LoadFromFile_returns_no_data_the_expected_error_message_is_returned()
        {
            // Arrange
            var args = GenerateArgs();
            var command = CrateCommand();

            _mockFileImportService.Setup(x => x.LoadFromFile(It.IsAny<string>()))
                .Returns(new Result() { Data = null });

            // Act
            var result = command.Execute(args);

            // Assert
            result.Messages[0].Should().Be("File import result is unknown");
        }

        [TestMethod]
        public void When_FileImportService_LoadFromFile_returns_no_data_in_an_unexpected_format_the_expected_error_message_is_returned()
        {
            // Arrange
            var args = GenerateArgs();
            var command = CrateCommand();

            _mockFileImportService.Setup(x => x.LoadFromFile(It.IsAny<string>()))
                .Returns(new Result() { Data = "An unexpected type" });

            // Act
            var result = command.Execute(args);

            // Assert
            result.Messages[0].Should().Be("File import result is unknown");
        }
    }
}
