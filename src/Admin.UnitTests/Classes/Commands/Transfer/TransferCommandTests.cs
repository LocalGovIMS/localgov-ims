using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.Transfer.TransferCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.Transfer.TransferViewModel;

namespace Admin.UnitTests.Classes.Commands.Transfer
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TransferCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransferService> _mockTransactionTransferService = new Mock<ITransferService>();

        private void SetupTransactionTransferService(Mock<ITransferService> service)
        {
            service.Setup(x => x.Transfer(
                It.IsAny<List<TransferItem>>(),
                It.IsAny<TransferItem>())).Returns(new Result());
        }

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Funds = null,
                SourceItem = null,
                TransferItem = null,
                TransferItems = null
            };
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            SetupTransactionTransferService(_mockTransactionTransferService);

            var command = new Command(
                _mockLogger.Object,
                _mockTransactionTransferService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
