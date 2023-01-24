using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Extensions.Import
{
    [TestClass]
    public class HasErrorsTests : TestBase
    {
        [TestMethod]
        public void HasErrors_OnAImportWithErrors_ReturnsTrue()
        {
            // Arrange
            var import = GetImportWithErrors();

            // Act
            var result = import.HasErrors();

            // Assert
            result
                .Should()
                .BeTrue();
        }

        [TestMethod]
        public void HasErrors_OnAImportWithoutErrors_ReturnsFalse()
        {
            // Arrange
            var import = GetImport();

            // Act
            var result = import.HasErrors();

            // Assert
            result
                .Should()
                .BeFalse();
        }
    }
}
