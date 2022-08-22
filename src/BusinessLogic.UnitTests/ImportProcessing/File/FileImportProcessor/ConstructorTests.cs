using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessLogic.UnitTests.ImportProcessing.File.FileImportProcessor
{
    [TestClass]
    public class ConstructorTests : TestBase
    {
        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenLoggerIsNull()
        {
            try
            {
                var fileImportProcessor = new BusinessLogic.ImportProcessing.FileImportProcessor(
                    null,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    MockSuspenseService.Object,
                    MockProcessedTransactionModelBuilder.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionMessageWhenLoggerIsNull()
        {
            try
            {
                var fileImportProcessor = new BusinessLogic.ImportProcessing.FileImportProcessor(
                    null,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    MockSuspenseService.Object,
                    MockProcessedTransactionModelBuilder.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: log");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenSecurityContextIsNull()
        {
            try
            {
                var fileImportProcessor = new BusinessLogic.ImportProcessing.FileImportProcessor(
                    MockLogger.Object,
                    null,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    MockSuspenseService.Object,
                    MockProcessedTransactionModelBuilder.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionMessageWhenSecurityContextIsNull()
        {
            try
            {
                var fileImportProcessor = new BusinessLogic.ImportProcessing.FileImportProcessor(
                    MockLogger.Object,
                    null,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    MockSuspenseService.Object,
                    MockProcessedTransactionModelBuilder.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: securityContext");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenUnitOfWorkIsNull()
        {
            try
            {
                var fileImportProcessor = new BusinessLogic.ImportProcessing.FileImportProcessor(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    null,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    MockSuspenseService.Object,
                    MockProcessedTransactionModelBuilder.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionMessageWhenUnitOfWorkIsNull()
        {
            try
            {
                var fileImportProcessor = new BusinessLogic.ImportProcessing.FileImportProcessor(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    null,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    MockSuspenseService.Object,
                    MockProcessedTransactionModelBuilder.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: unitOfWork");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenRuleEngineIsNull()
        {
            try
            {
                var fileImportProcessor = new BusinessLogic.ImportProcessing.FileImportProcessor(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    null,
                    MockTransactionService.Object,
                    MockSuspenseService.Object,
                    MockProcessedTransactionModelBuilder.Object);

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
                var fileImportProcessor = new BusinessLogic.ImportProcessing.FileImportProcessor(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    null,
                    MockTransactionService.Object,
                    MockSuspenseService.Object,
                    MockProcessedTransactionModelBuilder.Object);
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
                var fileImportProcessor = new BusinessLogic.ImportProcessing.FileImportProcessor(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    null,
                    MockSuspenseService.Object,
                    MockProcessedTransactionModelBuilder.Object);

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
                var fileImportProcessor = new BusinessLogic.ImportProcessing.FileImportProcessor(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    null,
                    MockSuspenseService.Object,
                    MockProcessedTransactionModelBuilder.Object);
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
                var fileImportProcessor = new BusinessLogic.ImportProcessing.FileImportProcessor(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    null,
                    MockProcessedTransactionModelBuilder.Object);

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
                var fileImportProcessor = new BusinessLogic.ImportProcessing.FileImportProcessor(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    null,
                    MockProcessedTransactionModelBuilder.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: suspenseService");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenProcessedTransactionModelBuilderIsNull()
        {
            try
            {
                var fileImportProcessor = new BusinessLogic.ImportProcessing.FileImportProcessor(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    MockSuspenseService.Object,
                    null);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionMessageWhenProcessedTransactionModelBuilderIsNull()
        {
            try
            {
                var fileImportProcessor = new BusinessLogic.ImportProcessing.FileImportProcessor(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    MockSuspenseService.Object,
                    null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: processedTransactionModelBuilder");
            }
        }
    }
}
