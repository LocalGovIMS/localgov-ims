using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.EReturnNote.CreateCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.EReturnNote.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.EReturnNote
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IEReturnNoteService> _mockEReturnNoteService = new Mock<IEReturnNoteService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Note = "A note",
                EReturnId = 1
            };
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockEReturnNoteService.Object);

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
            _mockEReturnNoteService.Setup(x => x.Create(It.IsAny<BusinessLogic.Entities.EReturnNote>()))
                .Returns(new Result());

            var command = new Command(
                _mockLogger.Object,
                _mockEReturnNoteService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
