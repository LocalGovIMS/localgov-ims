using BusinessLogic.Enums;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Enums
{
    [TestClass]
    public class LogicalOperationTypeTests
    {
        [TestMethod]
        public void And_has_the_expected_string_value()
        {
            // Arrange

            // Act

            // Assert
            LogicalOperationType.And.Should().Be("AND");
        }

        [TestMethod]
        public void Or_has_the_expected_string_value()
        {
            // Arrange

            // Act

            // Assert
            LogicalOperationType.Or.Should().Be("OR");
        }
    }
}
