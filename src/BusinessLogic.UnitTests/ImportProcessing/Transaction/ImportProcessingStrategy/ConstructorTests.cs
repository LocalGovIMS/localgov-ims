using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessLogic.UnitTests.ImportProcessing.Transaction.ImportProcessingStrategy
{
    [TestClass]
    public class ConstructorTests : TestBase
    {
        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenRuleEngineIsNull()
        {
            try
            {
                var instance = new BusinessLogic.ImportProcessing.TransactionImportProcessingStrategy(
                    null, 
                    MockTransactionService.Object,
                    MockSuspenseService.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionMessageWhenRuleEngineIsNull()
        {
            try
            {
                var instance = new BusinessLogic.ImportProcessing.TransactionImportProcessingStrategy(
                    null,
                    MockTransactionService.Object,
                    MockSuspenseService.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: ruleEngine");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenTransactionServiceIsNull()
        {
            try
            {
                var instance = new BusinessLogic.ImportProcessing.TransactionImportProcessingStrategy(
                    MockRuleEngine.Object,
                    null,
                    MockSuspenseService.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionMessageWhenTransactionServiceIsNull()
        {
            try
            {
                var instance = new BusinessLogic.ImportProcessing.TransactionImportProcessingStrategy(
                    MockRuleEngine.Object,
                    null,
                    MockSuspenseService.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: transactionService");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenSuspenseServiceIsNull()
        {
            try
            {
                var instance = new BusinessLogic.ImportProcessing.TransactionImportProcessingStrategy(
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    null);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionMessageWhenSuspenseServiceIsNull()
        {
            try
            {
                var instance = new BusinessLogic.ImportProcessing.TransactionImportProcessingStrategy(
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: suspenseService");
            }
        }
    }
}
