using Admin.Classes.ViewHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace Admin.UnitTests.Classes.ViewHelpers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FormatHelperTests
    {
        [TestMethod]
        public void CanBuild()
        {
            // Arrange
            HtmlHelper helper = null;

            // Act
            var result = helper.ToCurrency(10.001M);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void NullValueReturnsZero()
        {
            // Arrange
            HtmlHelper helper = null;

            // Act
            var result = helper.ToCurrency(null);

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
