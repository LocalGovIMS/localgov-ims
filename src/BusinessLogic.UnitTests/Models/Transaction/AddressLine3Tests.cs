using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    [TestClass]
    public class AddressLine3Tests : TestBase
    {
        [TestMethod]
        public void AddressLine3_returns_the_expected_value()
        {
            // Arrange
            var transaction = GetTransaction();

            // Act
            var value = transaction.AddressLine3;

            // Assert
            value.Should().Be(CardHolderAddressLine3);
        }
    }
}
