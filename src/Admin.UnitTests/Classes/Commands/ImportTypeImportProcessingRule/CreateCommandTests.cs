using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.ImportTypeImportProcessingRule.CreateCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.ImportTypeImportProcessingRule.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.ImportTypeImportProcessingRule
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreateCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportTypeImportProcessingRuleService> _mockImportTypeImportProcessingRuleService = new Mock<IImportTypeImportProcessingRuleService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                ImportProcessingRuleId = 1,
                ImportTypeId = 0,
                ImportProcessingRules = new Web.Mvc.SelectList()
            };
        }

        [TestMethod]
        public void Execute_return_a_CommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockImportTypeImportProcessingRuleService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void Execute_calls_ImportTypeImportProcessingRuleService_Create_method_once()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockImportTypeImportProcessingRuleService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            _mockImportTypeImportProcessingRuleService.Verify(x => x.Create(It.IsAny<BusinessLogic.Entities.ImportTypeImportProcessingRule>()), Times.Once);
        }
    }
}
