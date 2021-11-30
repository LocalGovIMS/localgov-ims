using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Extensions.ImportProcessingRuleCondition
{
    [TestClass]
    public class DisplaySummaryTests
    {
        [TestMethod]
        [DataRow("Field Display Name", "Operator Display Name", "Value", "Field display name must operator display name 'Value'")]
        public void DisplaySummary_returns_the_expected_summary(string fieldDisplayName, string operatorDisplayName, string value, string expectedResult)
        {
            // Arrange
            var importProcessingRuleCondition = new Entities.ImportProcessingRuleCondition()
            {
                Field = new Entities.ImportProcessingRuleField()
                {
                    DisplayName = fieldDisplayName
                },
                Operator = new Entities.ImportProcessingRuleOperator()
                {
                    DisplayName = operatorDisplayName
                },
                Value = value
            };

            // Act
            var result = importProcessingRuleCondition.DisplaySummary();

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void DisplaySummary_returns_an_empty_string_when_the_Field_property_is_null()
        {
            // Arrange
            var importProcessingRuleAction = new Entities.ImportProcessingRuleCondition()
            {
                Field = null,
                Operator = new Entities.ImportProcessingRuleOperator(),
                Value = "value"
            };

            // Act
            var result = importProcessingRuleAction.DisplaySummary();

            // Assert
            result.Should().Be(string.Empty);
        }

        [TestMethod]
        public void DisplaySummary_returns_an_empty_string_when_the_Operator_property_is_null()
        {
            // Arrange
            var importProcessingRuleAction = new Entities.ImportProcessingRuleCondition()
            {
                Field = new Entities.ImportProcessingRuleField(),
                Operator = null,
                Value = "value"
            };

            // Act
            var result = importProcessingRuleAction.DisplaySummary();

            // Assert
            result.Should().Be(string.Empty);
        }
    }
}
