using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.VatMetadata.DeleteCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;

namespace Admin.UnitTests.Classes.Commands.VatMetadata
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DeleteCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IVatMetadataService> _mockVatMetadataService = new Mock<IVatMetadataService>();

        [TestMethod]
        public void Execute_return_a_CommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockVatMetadataService.Object);

            // Act
            var result = command.Execute(1);

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
                _mockVatMetadataService.Object);

            // Act
            var result = command.Execute(1);

            // Assert
            _mockVatMetadataService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}
