using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Extensions.String
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class IsNumericTests
    {
        [TestMethod]
        [DataRow("ABCDEFG", false)]
        [DataRow("abcdefg", false)]
        [DataRow("ABCdefg", false)]
        [DataRow("ABCDEF1", false)]
        [DataRow("1BCDEFG", false)]
        [DataRow("ABCDEF/", false)]
        [DataRow("/%$£%%$£", false)]
        [DataRow("One", false)]
        [DataRow("1", true)]
        [DataRow("75437895378", true)]
        [DataRow("58954357867856   654783 543875 43678", false)]
        [DataRow(" ", false)]
        [DataRow("", false)]
        [DataRow(null, false)]
        public void IsNumeric_returns_the_expected_value(string value, bool expectedResult)
        {
            // Arrange
            
            // Act
            var result = value.IsNumeric();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
