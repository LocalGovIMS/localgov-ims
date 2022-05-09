using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Extensions.CheckDigitConfiguration
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ResultSubstitutionsDictionaryTests
    {
        [TestMethod]
        [DataRow("10:A,11:X", 2)]
        [DataRow("10:A,11:X,12:Y", 3)]
        [DataRow("", 0)]
        [DataRow(" ", 0)]
        [DataRow(null, 0)]
        public void ResultSubstitutionsDictionary_returns_the_expected_number_of_values(string substitutions, int expectedResult)
        {
            // Arrange
            var configuration = new Entities.CheckDigitConfiguration()
            {
                ResultSubstitutions = substitutions
            };

            // Act
            var result = configuration.ResultSubstitutionsDictionary();

            // Assert
            result.Count.Should().Be(expectedResult);
        }
    }
}
