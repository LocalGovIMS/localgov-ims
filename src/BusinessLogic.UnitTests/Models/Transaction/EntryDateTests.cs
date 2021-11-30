using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    [TestClass]
    public class EntryDateTests : TestBase
    {
        [TestMethod]
        public void EntryDate_returns_the_expected_value()
        {
            // Arrange
            var transaction = GetTransaction();

            // Act
            var value = transaction.EntryDate;

            // Assert
            value.Should().Be(LatestEntryDate);
        }
    }
}
