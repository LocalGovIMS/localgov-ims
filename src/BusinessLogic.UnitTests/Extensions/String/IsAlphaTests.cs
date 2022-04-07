using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Extensions.String
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class IsAlphaTests
    {
        [TestMethod]
        [DataRow("ABCDEFG", true)]
        [DataRow("abcdefg", true)]
        [DataRow("ABCdefg", true)]
        [DataRow("ABCDEF1", false)]
        [DataRow("1BCDEFG", false)]
        [DataRow("ABCDEF/", false)]
        [DataRow("/%$£%%$£", false)]
        [DataRow("One", true)]
        [DataRow("1", false)]
        [DataRow("75437895378", false)]
        [DataRow("58954357867856   654783 543875 43678", false)]
        [DataRow(" ", false)]
        [DataRow("", false)]
        [DataRow(null, false)]
        public void IsAlpha_returns_the_expected_value(string value, bool expectedResult)
        {
            // Arrange
            
            // Act
            var result = value.IsAlpha();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
