using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.Suspense.SaveNoteCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.Suspense.SaveNoteViewModel;

namespace Admin.UnitTests.Classes.Commands.Suspense
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TransferCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ISuspenseService> _mockSuspenseService = new Mock<ISuspenseService>();

        private void SetupSuspenseService(Mock<ISuspenseService> service)
        {
            service.Setup(x => x.SaveNotes(
                It.IsAny<int>(),
                It.IsAny<string>())).Returns(new Result());
        }

        private ViewModel GenerateViewModel()
        {
            return new ViewModel();
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            SetupSuspenseService(_mockSuspenseService);

            var command = new Command(
                _mockLogger.Object,
                _mockSuspenseService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
