using BusinessLogic.Classes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Classes
{
    [TestClass]
    public class RefundRequestTests
    {
        [TestMethod]
        public void Constructor_sets_properties_correctly()
        {
            // Arrange

            // Act
            var refundRequest = new RefundRequest("Reference", 10.00M);

            // Assert
            refundRequest.TransactionReference.Should().Be("Reference");
            refundRequest.RefundAmount.Should().Be(10.0M);
        }
    }
}
