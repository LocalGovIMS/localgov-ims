using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Extensions.AccountHolder
{
    [TestClass]
    public class IsOnStopTests
    {
        [TestMethod]
        [DataRow(null, false)]
        [DataRow(1, true)]
        public void IsOnStop_returns_expected_value(int? fundMessageId, bool expectedResult)
        {
            // Arrange
            var accountHolder = new Entities.AccountHolder() { FundMessageId = fundMessageId };

            // Act
            var result = accountHolder.IsOnStop();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
