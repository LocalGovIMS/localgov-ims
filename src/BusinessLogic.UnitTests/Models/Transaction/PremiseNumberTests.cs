using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    [TestClass]
    public class PremiseNumberTests : TestBase
    {
        [TestMethod]
        public void PremiseNumber_returns_the_expected_value()
        {
            // Arrange
            var transaction = GetTransaction();

            // Act
            var value = transaction.PremiseNumber;

            // Assert
            value.Should().Be(CardHolderPremiseNumber);
        }
    }
}
