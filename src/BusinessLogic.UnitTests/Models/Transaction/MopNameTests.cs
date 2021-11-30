using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    [TestClass]
    public class MopNameTests : TestBase
    {
        [TestMethod]
        public void MopName_returns_the_expected_value()
        {
            // Arrange
            var transaction = GetTransaction();

            // Act
            var value = transaction.MopName;

            // Assert
            value.Should().Be($"{MopName} ({MopCode})");
        }
    }
}
