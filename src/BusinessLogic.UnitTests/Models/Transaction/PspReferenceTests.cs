using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Models.Transaction
{
    [TestClass]
    public class PspReferenceTests : TestBase
    {
        [TestMethod]
        public void PspReference_returns_the_expected_value()
        {
            // Arrange
            var transaction = GetTransaction();

            // Act
            var value = transaction.PspReference;

            // Assert
            value.Should().Be(PspReference);
        }
    }
}
