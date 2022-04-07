using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Extensions.Int
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class IsEvenTests
    {
        [TestMethod]
        [DataRow(-2, true)]
        [DataRow(-1, false)]
        [DataRow(0, true)]
        [DataRow(1, false)]
        [DataRow(2, true)]
        public void IsEven_returns_the_expected_value(int value, bool expectedResult)
        {
            // Arrange
            
            // Act
            var result = value.IsEven();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
