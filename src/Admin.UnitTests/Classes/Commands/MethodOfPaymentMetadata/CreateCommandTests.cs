using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.MethodOfPaymentMetadata.CreateCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.MethodOfPaymentMetadata.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.MethodOfPaymentMetadata
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IMethodOfPaymentMetadataService> _mockMethodOfPaymentMetadataService = new Mock<IMethodOfPaymentMetadataService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                MetadataKeyName = "Test key",
                Value = "Test value"
            };
        }

        [TestMethod]
        public void Execute_return_a_CommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockMethodOfPaymentMetadataService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void Execute_calls_ImportProcessingRuleService_Create_method_once()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockMethodOfPaymentMetadataService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            _mockMethodOfPaymentMetadataService.Verify(x => x.Create(It.IsAny<BusinessLogic.Entities.MopMetadata>()), Times.Once);
        }
    }
}
