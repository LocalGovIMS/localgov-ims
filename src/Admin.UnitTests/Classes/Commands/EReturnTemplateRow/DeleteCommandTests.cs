using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.EReturnTemplateRow.DeleteCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;

namespace Admin.UnitTests.Classes.Commands.EReturnTemplateRow
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DeleteCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IEReturnTemplateRowService> _mockEReturnTemplateRowService = new Mock<IEReturnTemplateRowService>();

        [TestMethod]
        public void Execute_return_a_CommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockEReturnTemplateRowService.Object);

            // Act
            var result = command.Execute(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void Execute_calls_ImportProcessingRuleService_Delete_method_once()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockEReturnTemplateRowService.Object);

            // Act
            var result = command.Execute(1);

            // Assert
            _mockEReturnTemplateRowService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}
