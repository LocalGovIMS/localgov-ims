using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Web.Mvc.DataAnnotations;

namespace Web.UnitTests.Mvc.Attributes
{
    [TestClass]
    public class ModelClientValidationGreaterThanRuleTests
    {
        [TestMethod]
        [DataRow("An error message", "Something", true)]
        [DataRow("Another error message", "Something else", false)]
        public void Construction_creates_an_instance_in_the_expected_state(string errorMessage, string other, bool allowEquality)
        {
            // Arrange
            // Act
            var instance = new ModelClientValidationGreaterThanRule(errorMessage, other, allowEquality);

            // Assert
            instance.ErrorMessage.Should().Be(errorMessage);
            instance.ValidationType.Should().Be("greaterthan");
            instance.ValidationParameters["other"].Should().Be(other);
            instance.ValidationParameters["allowequality"].Should().Be(allowEquality);
        }
    }
}
