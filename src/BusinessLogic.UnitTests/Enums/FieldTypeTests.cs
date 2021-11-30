using BusinessLogic.Enums;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Enums
{
    [TestClass]
    public class FieldTypeTests
    {
        [TestMethod]
        public void Text_has_the_expected_string_value()
        {
            // Arrange

            // Act

            // Assert
            FieldType.Text.Should().Be("Text");
        }

        [TestMethod]
        public void Decimal_has_the_expected_string_value()
        {
            // Arrange

            // Act

            // Assert
            FieldType.Decimal.Should().Be("Decimal");
        }
    }
}
