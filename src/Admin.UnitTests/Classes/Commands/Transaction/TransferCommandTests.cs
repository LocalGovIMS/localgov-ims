using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.Transaction.TransferCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.Transaction.TransferViewModel;

namespace Admin.UnitTests.Classes.Commands.Transaction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TransferCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransactionJournalService> _mockTransactionJournalService = new Mock<ITransactionJournalService>();

        private void SetupTransactionJournalService(Mock<ITransactionJournalService> service)
        {
            service.Setup(x => x.Transfer(
                It.IsAny<List<TransferItem>>(),
                It.IsAny<string>(),
                It.IsAny<string>())).Returns(new Result());
        }

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Funds = null,
                PspReference = string.Empty,
                TransactionReference = string.Empty,
                TransferItem = null,
                TransferItems = null
            };
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
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
