using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    [TestClass]
    public class PostCodeTests : TestBase
    {
        [TestMethod]
        public void PostCode_returns_the_expected_value()
        {
            // Arrange
            var transaction = GetTransaction();

            // Act
            var value = transaction.PostCode;

            // Assert
            value.Should().Be(CardHolderPostCode);
        }
    }
}