using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.Import
{
    [TestClass]
    public class HasErrorsTests : TestBase
    {
        [TestMethod]
        public void HasErrors_OnAImportWithErrors_ReturnsTrue()
        {
            // Arrange
            var Import = GetImportWithErrors();

            // Act
            var result = Import.HasErrors();

            // Assert
            result
                .Should()
                .BeTrue();
        }

        [TestMethod]
        public void HasErrors_OnAImportWithoutErrors_ReturnsFalse()
        {
            // Arrange
            var Import = GetImport();

            // Act
            var result = Import.HasErrors();

            // Assert
            result
                .Should()
                .BeFalse();
        }
    }
}
