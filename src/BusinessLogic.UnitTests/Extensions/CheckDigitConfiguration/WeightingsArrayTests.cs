using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Extensions.CheckDigitConfiguration
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class WeightingsArrayTests
    {
        [TestMethod]
        [DataRow("1,2,3,4,5,6,7,8,9", 9)]
        [DataRow("10,20,30,40", 4)]
        [DataRow("", 0)]
        [DataRow(" ", 0)]
        [DataRow(null, 0)]
        public void WeightingsArray_contains_the_correct_number_of_elements(string weightings, int expectedResult)
        {
            // Arrange
            var configuration = new Entities.CheckDigitConfiguration()
            {
                Weightings = weightings
            };

            // Act
            var result = configuration.WeightingsArray();

            // Assert
            result.Length.Should().Be(expectedResult);
        }

    }
}
