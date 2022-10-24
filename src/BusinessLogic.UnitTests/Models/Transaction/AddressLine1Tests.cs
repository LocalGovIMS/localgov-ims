using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    [TestClass]
    public class AddressLine1Tests : TestBase
    {
        [TestMethod]
        public void AddressLine1_returns_the_expected_value()
        {
            // Arrange
            var transaction = GetTransaction();

            // Act
            var value = transaction.AddressLine1;

            // Assert
            value.Should().Be(CardHolderAddressLine1);
        }
    }
}
