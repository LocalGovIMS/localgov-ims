using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.ImportProcessingRuleAction.CreateCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.ImportProcessingRuleAction.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.ImportProcessingRuleAction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportProcessingRuleActionService> _mockImportProcessingRuleActionService = new Mock<IImportProcessingRuleActionService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                ImportProcessingRuleId = 1,
                ImportProcessingRuleFieldId = 1,
                Value = "Test value"
            };
        }

        [TestMethod]
        public void Execute_return_a_CommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockImportProcessingRuleActionService.Object);

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
                _mockImportProcessingRuleActionService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            _mockImportProcessingRuleActionService.Verify(x => x.Create(It.IsAny<BusinessLogic.Entities.ImportProcessingRuleAction>()), Times.Once);
        }
    }
}
