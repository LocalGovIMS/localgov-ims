using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.ImportProcessingRuleTransactionImportType.DeleteCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;

namespace Admin.UnitTests.Classes.Commands.ImportProcessingRuleTransactionImportType
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class DeleteCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransactionImportTypeImportProcessingRuleService> _mockTransactionImportTypeImportProcessingRuleService = new Mock<ITransactionImportTypeImportProcessingRuleService>();

        [TestMethod]
        public void Execute_return_a_CommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockTransactionImportTypeImportProcessingRuleService.Object);

            // Act
            var result = command.Execute(1);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void Execute_calls_TransacitonImportTypeImportProcessingRuleService_Delete_method_once()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockTransactionImportTypeImportProcessingRuleService.Object);

            // Act
            var result = command.Execute(1);

            // Assert
            _mockTransactionImportTypeImportProcessingRuleService.Verify(x => x.Delete(It.IsAny<int>()), Times.Once);
        }
    }
}
