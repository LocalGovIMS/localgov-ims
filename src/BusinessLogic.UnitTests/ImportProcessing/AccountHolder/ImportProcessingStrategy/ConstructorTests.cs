using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessLogic.UnitTests.ImportProcessing.AccountHolder.ImportProcessingStrategy
{
    [TestClass]
    public class ConstructorTests : TestBase
    {
        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenAccountHolderRepositoryIsNull()
        {
            try
            {
                var instance = new BusinessLogic.ImportProcessing.AccountHolderImportProcessingStrategy(
                    null,
                    MockSecurityContext.Object);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionMessageWhenAccountHolderRepositoryIsNull()
        {
            try
            {
                var instance = new BusinessLogic.ImportProcessing.AccountHolderImportProcessingStrategy(
                    null,
                    MockSecurityContext.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: accountHolderRepository");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenSecurityContextIsNull()
        {
            try
            {
                var instance = new BusinessLogic.ImportProcessing.AccountHolderImportProcessingStrategy(
                    MockAccountHolderRepository.Object,
                    null);

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
                var instance = new BusinessLogic.ImportProcessing.AccountHolderImportProcessingStrategy(
                    MockAccountHolderRepository.Object,
                    null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: securityContext");
            }
        }
    }
}
