﻿using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.ImportProcessingRuleAction.EditCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.ImportProcessingRuleAction.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.ImportProcessingRuleAction
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IImportProcessingRuleActionService> _mockImportProcessingRuleActionService = new Mock<IImportProcessingRuleActionService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Id = 1,
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
        public void Execute_calls_ImportProcessingRuleService_Update_method_once()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockImportProcessingRuleActionService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            _mockImportProcessingRuleActionService.Verify(x => x.Update(It.IsAny<BusinessLogic.Entities.ImportProcessingRuleAction>()), Times.Once);
        }
    }
}
