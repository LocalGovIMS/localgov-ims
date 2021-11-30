using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.Transaction.UndoTransferCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;

namespace Admin.UnitTests.Classes.Commands.Transaction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class UndoTransferCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransactionJournalService> _mockTransactionJournalService = new Mock<ITransactionJournalService>();

        private void SetupTransactionJournalService(Mock<ITransactionJournalService> service)
        {
            service.Setup(x => x.UndoTransfer(It.IsAny<string>())).Returns(new Result());
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            SetupTransactionJournalService(_mockTransactionJournalService);

            var command = new Command(
                _mockLogger.Object,
                _mockTransactionJournalService.Object);

            // Act
            var result = command.Execute("test");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
