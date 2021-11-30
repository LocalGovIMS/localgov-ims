using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    [TestClass]
    public class StreetTests : TestBase
    {
        [TestMethod]
        public void Street_returns_the_expected_value()
        {
            // Arrange
            var transaction = GetTransaction();

            // Act
            var value = transaction.Street;

            // Assert
            value.Should().Be(CardHolderStreet);
        }
    }
}
