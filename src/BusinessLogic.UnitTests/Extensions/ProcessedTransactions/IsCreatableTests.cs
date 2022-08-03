using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Extensions.ProcessedTransaction
{
    [TestClass]
    public class IsCreatableTests
    {
        [TestMethod]
        [DataRow("")]
        [DataRow(null)]
        public void IsCreatable_WhenAFundCodeIsNullOrEmpty_ReturnsFalse(string fundCode)
        {
            // Arrange
            var processedTransaction = TestData.ProcessedTransaction.Get();
            processedTransaction.FundCode = fundCode;

            // Act
            var result = processedTransaction.IsCreatable();

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        [DataRow("A1")]
        [DataRow("B1")]
        public void IsCreatable_WhenAFundCodeIsProvided_ReturnsTrue(string fundCode)
        {
            // Arrange
            var processedTransaction = TestData.ProcessedTransaction.Get();
            processedTransaction.FundCode = fundCode;

            // Act
            var result = processedTransaction.IsCreatable();

            // Assert
            result.Should().BeTrue();
        }
    }
}
