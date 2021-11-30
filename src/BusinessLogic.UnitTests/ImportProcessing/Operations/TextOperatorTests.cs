using BusinessLogic.ImportProcessing;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.ImportProcessing.Operations
{
    [TestClass]
    public class TextOperatorTests
    {
        private readonly BusinessLogic.ImportProcessing.Operations _operations;

        public TextOperatorTests()
        {
            _operations = new BusinessLogic.ImportProcessing.Operations();
        }

        [TestMethod]
        [DataRow("A test field value", "ABC", false)]
        [DataRow("A test field value", "fieldvalue", false)]
        [DataRow("A test field value", "A", true)]
        [DataRow("A test field value", "test", true)]
        [DataRow("A test field value", "field", true)]
        [DataRow("A test field value", "value", true)]
        [DataRow("A test field value", "A test field value", true)]
        public void Contain_returns_the_expected_result(string fieldValue, string value, bool expectedResult)
        {
            // Arrange

            var args = new OperationArgs() { FieldValue = fieldValue, Value = value };

            // Act
            var result = _operations.TextOperators[OperationType.Contain](args);

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        [DataRow("A test field value", "ABC", true)]
        [DataRow("A test field value", "fieldvalue", true)]
        [DataRow("A test field value", "A", false)]
        [DataRow("A test field value", "test", false)]
        [DataRow("A test field value", "field", false)]
        [DataRow("A test field value", "value", false)]
        [DataRow("A test field value", "A test field value", false)]
        public void NotContain_returns_the_expected_result(string fieldValue, string value, bool expectedResult)
        {
            // Arrange
            var args = new OperationArgs() { FieldValue = fieldValue, Value = value };

            // Act
            var result = _operations.TextOperators[OperationType.NotContain](args);

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        [DataRow("A test field value", "ABC", false)]
        [DataRow("A test field value", "fieldvalue", false)]
        [DataRow("A test field value", "A", true)]
        [DataRow("A test field value", "test", false)]
        [DataRow("A test field value", "field", false)]
        [DataRow("A test field value", "value", false)]
        [DataRow("A test field value", "A test field value", true)]
        public void StartWith_returns_the_expected_result(string fieldValue, string value, bool expectedResult)
        {
            // Arrange
            var args = new OperationArgs() { FieldValue = fieldValue, Value = value };

            // Act
            var result = _operations.TextOperators[OperationType.StartWith](args);

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        [DataRow("A test field value", "ABC", false)]
        [DataRow("A test field value", "fieldvalue", false)]
        [DataRow("A test field value", "A", false)]
        [DataRow("A test field value", "test", false)]
        [DataRow("A test field value", "field", false)]
        [DataRow("A test field value", "value", true)]
        [DataRow("A test field value", "A test field value", true)]
        public void EndWith_returns_the_expected_result(string fieldValue, string value, bool expectedResult)
        {
            // Arrange
            var args = new OperationArgs() { FieldValue = fieldValue, Value = value };

            // Act
            var result = _operations.TextOperators[OperationType.EndWith](args);

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        [DataRow("A test field value", "ABC", false)]
        [DataRow("A test field value", "fieldvalue", false)]
        [DataRow("A test field value", "A", false)]
        [DataRow("A test field value", "test", false)]
        [DataRow("A test field value", "field", false)]
        [DataRow("A test field value", "value", false)]
        [DataRow("A test field value", "A test field value", true)]
        public void Equal_returns_the_expected_result(string fieldValue, string value, bool expectedResult)
        {
            // Arrange
            var args = new OperationArgs() { FieldValue = fieldValue, Value = value };

            // Act
            var result = _operations.TextOperators[OperationType.Equal](args);

            // Assert
            result.Should().Be(expectedResult);
        }
    }
}
