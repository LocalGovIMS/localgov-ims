using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.Extensions.ImportProcessingRuleCondition
{
    [TestClass]
    public class IsNullOrEmptyTests
    {
        [TestMethod]
        public void IsNullOrEmptyTests_returns_the_false_when_value_is_null()
        {
            // Arrange
            IEnumerable<int> test = null;

            // Act
            var result = test.IsNullOrEmpty();

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void IsNullOrEmptyTests_returns_the_false_when_value_is_empty()
        {
            // Arrange
            IEnumerable<int> test = new List<int>();

            // Act
            var result = test.IsNullOrEmpty();

            // Assert
            result.Should().BeTrue();
        }

        [TestMethod]
        public void IsNullOrEmptyTests_returns_the_false_when_value_is_a_list_of_things()
        {
            // Arrange
            IEnumerable<int> test = new List<int>() { 1, 2, 3 };

            // Act
            var result = test.IsNullOrEmpty();

            // Assert
            result.Should().BeFalse();
        }
    }
}
