using BusinessLogic.ImportProcessing;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BusinessLogic.UnitTests.ImportProcessing.Operations
{
    [TestClass]
    public class DecimalOperatorTests
    {
        private readonly BusinessLogic.ImportProcessing.Operations _operations;

        public DecimalOperatorTests()
        {
            _operations = new BusinessLogic.ImportProcessing.Operations();
        }

        [TestMethod]
        [DataRow("0.9", "1", false)]
        [DataRow("1", "1", false)]
        [DataRow("1.1", "1", true)]
        public void BeGreaterThan_returns_the_expected_result(string fieldValue, string value, bool expectedResult)
        {
            // Arrange

            var args = new OperationArgs() { FieldValue = Convert.ToDecimal(fieldValue), Value = Convert.ToDecimal(value) };

            // Act
            var result = _operations.DecimalOperators[OperationType.BeGreaterThan](args);

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        [DataRow("0.9", "1", true)]
        [DataRow("1", "1", false)]
        [DataRow("1.1", "1", false)]
        public void BeLessThan_returns_the_expected_result(string fieldValue, string value, bool expectedResult)
        {
            // Arrange
            var args = new OperationArgs() { FieldValue = Convert.ToDecimal(fieldValue), Value = Convert.ToDecimal(value) };

            // Act
            var result = _operations.DecimalOperators[OperationType.BeLessThan](args);

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        [DataRow("0.9", "1", false)]
        [DataRow("1", "1", true)]
        [DataRow("1.1", "1", false)]
        public void Equal_returns_the_expected_result(string fieldValue, string value, bool expectedResult)
        {
            // Arrange
            var args = new OperationArgs() { FieldValue = Convert.ToDecimal(fieldValue), Value = Convert.ToDecimal(value) };

            // Act
            var result = _operations.DecimalOperators[OperationType.Equal](args);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
