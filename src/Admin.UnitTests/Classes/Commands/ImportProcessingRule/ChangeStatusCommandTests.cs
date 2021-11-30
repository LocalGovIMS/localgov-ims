using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.ImportProcessingRule.ChangeStatusCommand;
using CommandArgs = Admin.Classes.Commands.ImportProcessingRule.ChangeStatusCommandArgs;
using CommandResult = Admin.Classes.Commands.CommandResult;

namespace Admin.UnitTests.Classes.Commands.ImportProcessingRule
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ChangeStatusCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportProcessingRuleService> _mockImportProcessingRuleService = new Mock<IImportProcessingRuleService>();

        public ChangeStatusCommandTests()
        {
            _mockImportProcessingRuleService.Setup(x => x.Get(It.IsAny<int>()))
                .Returns(new BusinessLogic.Entities.ImportProcessingRule());
        }

        private CommandArgs GenerateArgs()
        {
            return new CommandArgs()
            {
                ImportProcessingRuleId = 1,
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
            var result = command.Execute(GenerateArgs());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void Execute_calls_ImportProcessingRuleService_Get_method_once()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockImportProcessingRuleService.Object);

            // Act
            var result = command.Execute(GenerateArgs());

            // Assert
            _mockImportProcessingRuleService.Verify(x => x.Get(It.IsAny<int>()), Times.Once);
        }

        [TestMethod]
        public void Execute_calls_ImportProcessingRuleService_Update_method_once()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockImportProcessingRuleService.Object);

            // Act
            var result = command.Execute(GenerateArgs());

            // Assert
            _mockImportProcessingRuleService.Verify(x => x.Update(It.IsAny<BusinessLogic.Entities.ImportProcessingRule>()), Times.Once);
        }
    }
}
