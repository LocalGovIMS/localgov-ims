using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    [TestClass]
    public class LastUpdatedDateTests : TestBase
    {
        [TestMethod]
        public void LastUpdatedDate_returns_the_expected_value()
        {
            // Arrange
            var transaction = GetTransaction();

            // Act
            var value = transaction.LastUpdatedDate;

            // Assert
            value.Should().Be(LastUpdatedDate);
        }
    }
}
