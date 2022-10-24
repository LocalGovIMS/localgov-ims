using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    [TestClass]
    public class AddressLine4Tests : TestBase
    {
        [TestMethod]
        public void Town_returns_the_expected_value()
        {
            // Arrange
            var transaction = GetTransaction();

            // Act
            var value = transaction.AddressLine4;

            // Assert
            value.Should().Be(CardHolderAddressLine4);
        }
    }
}
