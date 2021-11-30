using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Extensions.ImportProcessingRuleAction
{
    [TestClass]
    public class DisplaySummaryTests
    {
        [TestMethod]
        [DataRow("Display Name", "Value", "Display name to 'Value'")]
        public void DisplaySummary_returns_the_expected_summary(string displayName, string value, string expectedResult)
        {
            // Arrange
            var importProcessingRuleAction = new Entities.ImportProcessingRuleAction()
            {
                Field = new Entities.ImportProcessingRuleField()
                {
                    DisplayName = displayName
                },
                Value = value
            };

            // Act
            var result = importProcessingRuleAction.DisplaySummary();

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void DisplaySummary_returns_an_empty_string_when_the_Field_property_is_null()
        {
            // Arrange
            var importProcessingRuleAction = new Entities.ImportProcessingRuleAction()
            {
                Field = null,
                Value = "value"
            };

            // Act
            var result = importProcessingRuleAction.DisplaySummary();

            // Assert
            result.Should().Be(string.Empty);
        }
    }
}
