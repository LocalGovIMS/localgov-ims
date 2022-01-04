using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.SuspenseNote.CreateCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.SuspenseNote.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.SuspenseNote
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ISuspenseNoteService> _mockSuspenseNoteService = new Mock<ISuspenseNoteService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Note = "A note",
                SuspenseId = 1
            };
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockSuspenseNoteService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResultFromResult()
        {
            // Arrange
            _mockSuspenseNoteService.Setup(x => x.Create(It.IsAny<BusinessLogic.Entities.SuspenseNote>()))
                .Returns(new Result());

            var command = new Command(
                _mockLogger.Object,
                _mockSuspenseNoteService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
