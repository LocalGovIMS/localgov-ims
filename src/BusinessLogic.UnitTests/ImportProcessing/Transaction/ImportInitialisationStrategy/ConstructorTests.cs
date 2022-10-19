using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessLogic.UnitTests.ImportProcessing.Transaction.ImportInitialisationStrategy
{
    [TestClass]
    public class ConstructorTests : TestBase
    {
        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenMetadataKeyServiceIsNull()
        {
            try
            {
                var instance = new BusinessLogic.ImportProcessing.TransactionImportInitialisationStrategy(null);

            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionMessageWhenMetadataKeyServiceIsNull()
        {
            try
            {
                var instance = new BusinessLogic.ImportProcessing.TransactionImportInitialisationStrategy(null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: metadataKeyService");
            }
        }
    }
}
