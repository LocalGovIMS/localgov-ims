using BusinessLogic.Classes;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace BusinessLogic.UnitTests.Classes
{
    [TestClass]
    public class RefundStatusTests
    {
        [TestMethod]
        public void AcceptedStatus_sets_properties_correctly()
        {
            // Arrange

            // Act
            var refundStatus = RefundStatus.AcceptedStatus(10.00M);

            // Assert
            refundStatus.Status.Should().Be(RefundStatusType.Accepted);
            refundStatus.Amount.Should().Be(10.00M);
            refundStatus.Message.Should().BeNull();
            refundStatus.PspReference.Should().BeNull();
        }

        [TestMethod]
        public void SuccessStatus_sets_properties_correctly()
        {
            // Arrange

            // Act
            var refundStatus = RefundStatus.SuccessStatus("SuccessMessage", "PspReference", 10.00M);

            // Assert
            refundStatus.Status.Should().Be(RefundStatusType.Success);
            refundStatus.Message.Should().Be("SuccessMessage");
            refundStatus.PspReference.Should().Be("PspReference");
            refundStatus.Amount.Should().Be(10.0M);
        }

        [TestMethod]
        public void FailStatus_sets_properties_correctly()
        {
            // Arrange

            // Act
            var refundStatus = RefundStatus.FailStatus("FailMessage");

            // Assert
            refundStatus.Status.Should().Be(RefundStatusType.Failed);
            refundStatus.Message.Should().Be("FailMessage");
            refundStatus.PspReference.Should().BeNull();
            refundStatus.Amount.Should().BeNull();
        }

        [TestMethod]
        public void ErrorStatus_sets_properties_correctly()
        {
            // Arrange

            // Act
            var refundStatus = RefundStatus.ErrorStatus("ErrorMessage");

            // Assert
            refundStatus.Status.Should().Be(RefundStatusType.Error);
            refundStatus.Message.Should().Be("ErrorMessage");
            refundStatus.PspReference.Should().BeNull();
            refundStatus.Amount.Should().BeNull();
        }
    }
}
