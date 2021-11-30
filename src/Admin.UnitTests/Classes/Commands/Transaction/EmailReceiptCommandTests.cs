using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.Transaction.EmailReceiptCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.Transaction.EmailReceiptViewModel;

namespace Admin.UnitTests.Classes.Commands.Transaction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EmailReceiptCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IEmailService> _mockEmailService = new Mock<IEmailService>();
        private readonly Mock<ITransactionService> _mockTransactionService = new Mock<ITransactionService>();

        private void SetupEmailService(Mock<IEmailService> service)
        {
            service.Setup(x => x.SendVatReceiptEmail(
                It.IsAny<string>(),
                It.IsAny<BusinessLogic.Models.Transaction>())).Returns(new Result());
        }

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                EmailAddress = "Test",
                PspReference = "123456"
            };
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            SetupEmailService(_mockEmailService);

            var command = new Command(
                _mockLogger.Object,
                _mockEmailService.Object,
                _mockTransactionService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
