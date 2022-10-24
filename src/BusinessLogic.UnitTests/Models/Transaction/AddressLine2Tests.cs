using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    [TestClass]
    public class AddressLine2Tests : TestBase
    {
        [TestMethod]
        public void AddressLine2_returns_the_expected_value()
        {
            // Arrange
            var transaction = GetTransaction();

            // Act
            var value = transaction.AddressLine2;

            // Assert
            value.Should().Be(CardHolderAddressLine2);
        }
    }
}
