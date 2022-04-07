using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Extensions.Int
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class IsOddTests
    {
        [TestMethod]
        [DataRow(-2, false)]
        [DataRow(-1, true)]
        [DataRow(0, false)]
        [DataRow(1, true)]
        [DataRow(2, false)]
        public void IsOdd_returns_the_expected_value(int value, bool expectedResult)
        {
            // Arrange
            
            // Act
            var result = value.IsOdd();

            // Assert
            result.Should().Be(expectedResult);
        }

    }
}
