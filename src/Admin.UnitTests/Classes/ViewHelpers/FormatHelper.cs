using Admin.Classes.ViewHelpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;

namespace Admin.UnitTests.Classes.ViewHelpers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class FormatHelperTests
    {
        [DataRow("10", "10.00")]
        [DataRow("10.0", "10.00")]
        [DataRow("10.01", "10.01")]
        [DataRow("10.011", "10.01")]
        [DataRow("10.001", "10.00")]
        [DataRow("10.1", "10.10")]
        [DataRow("10.10", "10.10")]
        [DataRow("10.101", "10.10")]
        [TestMethod]
        public void ToCurrency_returns_the_expected_value(string value, string expectedResult)
        {
            // Arrange
            HtmlHelper helper = null;

            // Act
            var result = helper.ToCurrency(Convert.ToDecimal(value));

            // Assert
            Assert.AreEqual(expectedResult, result.ToString());
        }

        [TestMethod]
        public void ToCurrency_for_a_null_value_returns_zero()
        {
            // Arrange
            HtmlHelper helper = null;

            // Act
            var result = helper.ToCurrency(null);

            // Assert
            Assert.AreEqual("0.00", result.ToString());
        }

        [TestMethod]
        public void ToMaxLength_for_a_null_value_returns_and_empty_string()
        {
            // Arrange
            HtmlHelper helper = null;

            // Act
            var result = helper.ToMaxLength(null, 10);

            // Assert
            Assert.AreEqual(string.Empty, result.ToString());
        }

        [TestMethod]
        public void ToMaxLength_for_an_empty_string_value_returns_and_empty_string()
        {
            // Arrange
            HtmlHelper helper = null;

            // Act
            var result = helper.ToMaxLength(string.Empty, 10);

            // Assert
            Assert.AreEqual(string.Empty, result.ToString());
        }

        [DataRow("This is the input string", 10, "This is th...")]
        [DataRow("This is the input string", 12, "This is the...")]
        [DataRow("          ", 5, "...")]
        [TestMethod]
        public void ToMaxLength_returns_the_expected_value(string value, int length, string expectedResult)
        {
            // Arrange
            HtmlHelper helper = null;

            // Act
            var result = helper.ToMaxLength(value, length);

            // Assert
            Assert.AreEqual(expectedResult, result.ToString());
        }
    }
}
