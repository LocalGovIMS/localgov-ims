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
            var import = GetImportWithErrors();

            // Act
            var result = import.Errors();

            // Assert
            result.Count()
                .Should()
                .Be(2);
        }



        [TestMethod]
        public void Errors_OnAImportWithoutErrors_ReturnsTheExpectedNumberOfErrors()
        {
            // Arrange
            var import = GetImportWithInfo();

            // Act
            var result = import.Errors();

            // Assert
            result.Count()
                .Should()
                .Be(0);
        }
    }
}
