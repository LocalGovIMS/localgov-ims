using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.ImportProcessingRuleTransactionImportType.CreateCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.ImportProcessingRuleTransactionImportType.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.ImportProcessingRuleTransactionImportType
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransactionImportTypeImportProcessingRuleService> _mockTransactionImportTypeImportProcessingRuleService = new Mock<ITransactionImportTypeImportProcessingRuleService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                ImportProcessingRuleId = 1,
                TransactionImportTypeId = 0,
                TransactionImportTypes = new Web.Mvc.SelectList()
            };
        }

        [TestMethod]
        public void Execute_return_a_CommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockTransactionImportTypeImportProcessingRuleService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void Execute_calls_TransactionImportTypeImportProcessingRuleService_Create_method_once()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockTransactionImportTypeImportProcessingRuleService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            _mockTransactionImportTypeImportProcessingRuleService.Verify(x => x.Create(It.IsAny<BusinessLogic.Entities.TransactionImportTypeImportProcessingRule>()), Times.Once);
        }
    }
}
