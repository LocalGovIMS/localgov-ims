using Admin.Models.Shared;
using BusinessLogic.Classes;
using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.Transaction.RefundCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.Transaction.RefundViewModel;

namespace Admin.UnitTests.Classes.Commands.Transaction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RefundCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IRefundService> _mockRefundService = new Mock<IRefundService>();

        private void SetupRefundService(Mock<IRefundService> item, RefundStatusType refundStatusType)
        {
            item.Setup(x => x.RefundTransactions(
                It.IsAny<List<RefundRequest>>(),
                It.IsAny<string>())).Returns(new RefundStatus() { Status = refundStatusType });
        }

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Reference = "test",
                RefundItems = new List<Admin.Models.Transaction.RefundItem>() {
                    {
                        new Admin.Models.Transaction.RefundItem()
                        {
                            RefundAmount = 1,
                            RemainingAmount = 2,
                            Transaction = new BusinessLogic.Entities.ProcessedTransaction()
                            {
                                TransactionReference = "1"
                            }
                        }
                    }
                },
                RefundReason = "test"
            };
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            SetupRefundService(_mockRefundService, RefundStatusType.Success);

            var command = new Command(
                _mockLogger.Object,
                _mockRefundService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void CheckAcceptedMessage()
        {
            // Arrange
            SetupRefundService(_mockRefundService, RefundStatusType.Accepted);

            var command = new Command(
                _mockLogger.Object,
                _mockRefundService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.IsInstanceOfType(result.Data, typeof(SuccessMessage));
            Assert.AreEqual(((SuccessMessage)result.Data).Text, "Your refund is being processed, this may take several minutes, you can see the status below");
            Assert.AreEqual(((SuccessMessage)result.Data).Title, "We've requested your refund");
        }

        [TestMethod]
        public void CheckSuccessMessage()
        {
            // Arrange
            SetupRefundService(_mockRefundService, RefundStatusType.Success);

            var command = new Command(
                _mockLogger.Object,
                _mockRefundService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.IsInstanceOfType(result.Data, typeof(SuccessMessage));
            Assert.AreEqual(((SuccessMessage)result.Data).Text, "Your refund is being processed, this may take several minutes, you can see the status below");
            Assert.AreEqual(((SuccessMessage)result.Data).Title, "We've requested your refund");
        }

        [TestMethod]
        public void CheckErrorMessage()
        {
            // Arrange
            SetupRefundService(_mockRefundService, RefundStatusType.Error);

            var command = new Command(
                _mockLogger.Object,
                _mockRefundService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.IsInstanceOfType(result.Data, typeof(ErrorMessage));
            Assert.AreEqual(((ErrorMessage)result.Data).Title, "We couldn't process your refund");
        }

        [TestMethod]
        public void CheckFailedMessage()
        {
            // Arrange
            SetupRefundService(_mockRefundService, RefundStatusType.Failed);

            var command = new Command(
                _mockLogger.Object,
                _mockRefundService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.IsInstanceOfType(result.Data, typeof(ErrorMessage));
            Assert.AreEqual(((ErrorMessage)result.Data).Title, "We couldn't process your refund");
        }

    }
}
