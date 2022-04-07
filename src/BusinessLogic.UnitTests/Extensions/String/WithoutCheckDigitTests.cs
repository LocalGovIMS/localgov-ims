using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Extensions.String
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class WithoutCheckDigitTests
    {
        [TestMethod]
        [DataRow(null, null)]
        [DataRow("", "")]
        [DataRow(" ", " ")]
        [DataRow("1", "")]
        [DataRow("12", "1")]
        [DataRow("1X", "1")]
        public void WithoutCheckDigit_returns_the_expected_value(string value, string expectedResult)
        {
            // Arrange
            
            // Act
            var result = value.WithoutCheckDigit();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
