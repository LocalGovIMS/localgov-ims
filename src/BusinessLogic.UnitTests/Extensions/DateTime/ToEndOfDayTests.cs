using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessLogic.UnitTests.Extensions.DateTime
{
    [TestClass]
    public class ToEndOfDayTests
    {
        [TestMethod]
        [DataRow("01-MAR-2022 00:00:00.0000000", "01-MAR-2022 23:59:59.9999999")]
        [DataRow("01-MAR-2022 10:15:56.1234567", "01-MAR-2022 23:59:59.9999999")]
        [DataRow("01-MAR-2022 23:59:59.9999998", "01-MAR-2022 23:59:59.9999999")]
        public void ToEndOfData_returns_the_expected_result(string dateTime, string expectedDateTime)
        {
            // Arrange
            var source = Convert.ToDateTime(dateTime);
            var expectedResult = Convert.ToDateTime(expectedDateTime);

            // Act
            var result = source.ToEndOfDay();

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
