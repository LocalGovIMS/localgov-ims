using BusinessLogic.Classes.Result;
using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.PaymentIntegration.EditCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.PaymentIntegration.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.PaymentIntegration
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IPaymentIntegrationService> _mockPaymentIntegrationService = new Mock<IPaymentIntegrationService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Id = 1
            };
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockPaymentIntegrationService.Object);

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
            _mockPaymentIntegrationService.Setup(x => x.Create(It.IsAny<BusinessLogic.Entities.PaymentIntegration>()))
                .Returns(new Result());

            var command = new Command(
                _mockLogger.Object,
                _mockPaymentIntegrationService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }
    }
}
