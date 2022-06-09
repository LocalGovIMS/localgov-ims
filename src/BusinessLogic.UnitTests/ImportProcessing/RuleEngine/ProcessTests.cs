using BusinessLogic.Entities;
using BusinessLogic.ImportProcessing;
using BusinessLogic.Interfaces.Services;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.ImportProcessing.RuleEngine
{
    [TestClass]
    public class ProcessTests
    {
        private Mock<ILog> _mockLog = new Mock<ILog>();
        private Mock<IImportProcessingRuleService> _mockImportProcessingRuleService = new Mock<IImportProcessingRuleService>();
        private IOperations _operations = new BusinessLogic.ImportProcessing.Operations();
        private IRuleEngine _ruleEngine;

        [TestMethod]
        [DynamicData(nameof(TestHelpers.TestData), typeof(TestHelpers), DynamicDataSourceType.Property)]
        public void RuleEngine_produces_the_expected_output(List<ImportProcessingRule> rules, ProcessedTransaction transaction, string expectedFundCode)
        {
            // Arrange
            SetupRules(rules);
            SetupRuleEngine();

            // Act
            _ruleEngine.Process(transaction);

            // Assert
            transaction.FundCode.Should().Be(expectedFundCode);
        }

        private void SetupRules(List<ImportProcessingRule> rules)
        {
            _mockImportProcessingRuleService.Setup(x => x.GetAll(It.IsAny<bool>())).Returns(rules);
        }

        private void SetupRuleEngine()
        {
            _ruleEngine = new BusinessLogic.ImportProcessing.RuleEngine(
                _mockLog.Object,
                _mockImportProcessingRuleService.Object,
                _operations);
        }
    }
}
