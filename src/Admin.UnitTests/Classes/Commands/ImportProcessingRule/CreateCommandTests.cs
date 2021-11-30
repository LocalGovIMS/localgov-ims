using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.ImportProcessingRule.CreateCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.ImportProcessingRule.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.ImportProcessingRule
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportProcessingRuleService> _mockImportProcessingRuleService = new Mock<IImportProcessingRuleService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Name = "Test rule",
                Description = "Test rule description",
                IsDisabled = false
            };
        }

        [TestMethod]
        public void Execute_return_a_CommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockImportProcessingRuleService.Object);

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
                _mockImportProcessingRuleService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            _mockImportProcessingRuleService.Verify(x => x.Create(It.IsAny<BusinessLogic.Entities.ImportProcessingRule>()), Times.Once);
        }
    }
}
