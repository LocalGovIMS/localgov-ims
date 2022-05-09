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
        [DataRow("", false)]
        [DataRow(" ", false)]
        [DataRow("SM1", true)]
        public void IsOnStop_returns_expected_value(string stopMessageReference, bool expectedResult)
        {
            // Arrange
            var accountHolder = new Entities.AccountHolder() { StopMessageReference = stopMessageReference };

            // Act
            var result = accountHolder.IsOnStop();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
