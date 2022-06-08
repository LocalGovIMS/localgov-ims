﻿using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.TransactionImportTypeImportProcessingRule.EditCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.TransactionImportTypeImportProcessingRule.EditViewModel;

namespace Admin.UnitTests.Classes.Commands.TransactionImportTypeImportProcessingRule
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class EditCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ITransactionImportTypeImportProcessingRuleService> _mockTransactionImportTypeImportProcessingRuleService = new Mock<ITransactionImportTypeImportProcessingRuleService>();

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                Id = 1,
                ImportProcessingRuleId = 1,
                TransactionImportTypeId = 0,
                ImportProcessingRules = new Web.Mvc.SelectList()
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
        public void Execute_calls_TransactionImportTypeImportProcessingRuleService_Update_method_once()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockTransactionImportTypeImportProcessingRuleService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            _mockTransactionImportTypeImportProcessingRuleService.Verify(x => x.Update(It.IsAny<BusinessLogic.Entities.TransactionImportTypeImportProcessingRule>()), Times.Once);
        }
    }
}
