using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace BusinessLogic.UnitTests.Extensions.Import
{
    [TestClass]
    public class ErrorsTests : TestBase
    {
        [TestMethod]
        public void Errors_OnAImportWithErrors_ReturnsTheExpectedNumberOfErrors()
        {
            // Arrange
            var Import = GetImportWithErrors();

            // Act
            var result = Import.Errors();

            // Assert
            result.Count()
                .Should()
                .Be(2);
        }



        [TestMethod]
        public void Errors_OnAImportWithoutErrors_ReturnsTheExpectedNumberOfErrors()
        {
            // Arrange
            var Import = GetImportWithInfo();

            // Act
            var result = Import.Errors();

            // Assert
            result.Count()
                .Should()
                .Be(0);
        }
    }
}
