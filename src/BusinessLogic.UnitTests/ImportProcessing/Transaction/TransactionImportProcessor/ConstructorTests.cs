using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessLogic.UnitTests.ImportProcessing.Transaction.TransactionImportProcessor
{
    [TestClass]
    public class ConstructorTests : TestBase
    {
        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenLoggerIsNull()
        {
            try
            {
                var fileImport = new BusinessLogic.ImportProcessing.TransactionImportProcessor(
                    null,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    MockTransactionImportValidator.Object);

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
                var fileImport = new BusinessLogic.ImportProcessing.TransactionImportProcessor(
                    null,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    MockTransactionImportValidator.Object);
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
                var fileImport = new BusinessLogic.ImportProcessing.TransactionImportProcessor(
                    MockLog.Object,
                    null,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    MockTransactionImportValidator.Object);

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
                var fileImport = new BusinessLogic.ImportProcessing.TransactionImportProcessor(
                    MockLog.Object,
                    null,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    MockTransactionImportValidator.Object);
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
                var fileImport = new BusinessLogic.ImportProcessing.TransactionImportProcessor(
                    MockLog.Object,
                    MockSecurityContext.Object,
                    null,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    MockTransactionImportValidator.Object);

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
                var fileImport = new BusinessLogic.ImportProcessing.TransactionImportProcessor(
                    MockLog.Object,
                    MockSecurityContext.Object,
                    null,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    MockTransactionImportValidator.Object);
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
                var fileImport = new BusinessLogic.ImportProcessing.TransactionImportProcessor(
                    MockLog.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    null, 
                    MockTransactionService.Object,
                    MockTransactionImportValidator.Object);

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
                var fileImport = new BusinessLogic.ImportProcessing.TransactionImportProcessor(
                    MockLog.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    null,
                    MockTransactionService.Object,
                    MockTransactionImportValidator.Object);
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
                var fileImport = new BusinessLogic.ImportProcessing.TransactionImportProcessor(
                    MockLog.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    null,
                    MockTransactionImportValidator.Object);

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
                var fileImport = new BusinessLogic.ImportProcessing.TransactionImportProcessor(
                    MockLog.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    null,
                    MockTransactionImportValidator.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: transactionService");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenTransactionImportValidatorIsNull()
        {
            try
            {
                var fileImport = new BusinessLogic.ImportProcessing.TransactionImportProcessor(
                    MockLog.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
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
        public void ThrowsCorrectExceptionMessageWhenTransactionImportValidatorIsNull()
        {
            try
            {
                var fileImport = new BusinessLogic.ImportProcessing.TransactionImportProcessor(
                    MockLog.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: transactionImportValidator");
            }
        }
    }
}
