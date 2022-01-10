﻿using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessLogic.UnitTests.ImportProcessing.ImportProcessor
{
    public class ConstructorTests : TestBase
    {
        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenLoggerIsNull()
        {
            try
            {
                var fileImport = new BusinessLogic.ImportProcessing.ImportProcessor(
                    null,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
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
                var fileImport = new BusinessLogic.ImportProcessing.ImportProcessor(
                    null,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
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
                var fileImport = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLogger.Object,
                    null,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
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
                var fileImport = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLogger.Object,
                    null,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
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
                var fileImport = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    null,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
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
                var fileImport = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    null,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
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
                var fileImport = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    null,
                    MockTransactionService.Object,
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
                var fileImport = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    null,
                    MockTransactionService.Object,
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
                var fileImport = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    null,
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
                var fileImport = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    null,
                    MockProcessedTransactionModelBuilder.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: transactionService");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenProcessedTransactionModelBuilderIsNull()
        {
            try
            {
                var fileImport = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLogger.Object,
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
        public void ThrowsCorrectExceptionMessageWhenProcessedTransactionModelBuilderIsNull()
        {
            try
            {
                var fileImport = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLogger.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockRuleEngine.Object,
                    MockTransactionService.Object,
                    null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: processedTransactionModelBuilder");
            }
        }
    }
}