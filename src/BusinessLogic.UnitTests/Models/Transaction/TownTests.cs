using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    [TestClass]
    public class TownTests : TestBase
    {
        [TestMethod]
        public void Town_returns_the_expected_value()
        {
            // Arrange
            var transaction = GetTransaction();

            // Act
            var value = transaction.Town;

            // Assert
            value.Should().Be(CardHolderTown);
        }
    }
}
