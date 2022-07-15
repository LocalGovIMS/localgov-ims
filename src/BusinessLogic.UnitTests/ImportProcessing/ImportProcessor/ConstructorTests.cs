using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessLogic.UnitTests.ImportProcessing.ImportProcessor
{
    [TestClass]
    public class ConstructorTests : TestBase
    {
        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenLogIsNull()
        {
            try
            {
                var instance = new BusinessLogic.ImportProcessing.ImportProcessor(
                    null,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockImportProcessingStrategyFactory.Object,
                    MockImportProcessingValidatorFactory.Object);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionMessageWhenLogIsNull()
        {
            try
            {
                var instance = new BusinessLogic.ImportProcessing.ImportProcessor(
                    null,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockImportProcessingStrategyFactory.Object,
                    MockImportProcessingValidatorFactory.Object);
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
                var instance = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLog.Object,
                    null,
                    MockUnitOfWork.Object,
                    MockImportProcessingStrategyFactory.Object,
                    MockImportProcessingValidatorFactory.Object);
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
                var instance = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLog.Object,
                    null,
                    MockUnitOfWork.Object,
                    MockImportProcessingStrategyFactory.Object,
                    MockImportProcessingValidatorFactory.Object);
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
                var instance = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLog.Object,
                    MockSecurityContext.Object,
                    null,
                    MockImportProcessingStrategyFactory.Object,
                    MockImportProcessingValidatorFactory.Object);
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
                var instance = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLog.Object,
                    MockSecurityContext.Object,
                    null,
                    MockImportProcessingStrategyFactory.Object,
                    MockImportProcessingValidatorFactory.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: unitOfWork");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenImportProcessingStrategyFactoryIsNull()
        {
            try
            {
                var instance = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLog.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    null,
                    MockImportProcessingValidatorFactory.Object);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionMessageWhenImportProcessingStrategyFactoryIsNull()
        {
            try
            {
                var instance = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLog.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    null,
                    MockImportProcessingValidatorFactory.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: importProcessingStrategyFactory");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenImportProcessingValidatorFactoryIsNull()
        {
            try
            {
                var instance = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLog.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockImportProcessingStrategyFactory.Object,
                    null);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionMessageWhenImportProcessingValidatorFactoryIsNull()
        {
            try
            {
                var instance = new BusinessLogic.ImportProcessing.ImportProcessor(
                    MockLog.Object,
                    MockSecurityContext.Object,
                    MockUnitOfWork.Object,
                    MockImportProcessingStrategyFactory.Object,
                    null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: importProcessingValidatorFactory");
            }
        }
    }
}
