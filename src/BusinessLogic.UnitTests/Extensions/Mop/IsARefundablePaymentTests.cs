using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.Extensions.Mop
{
    [TestClass]
    public class IsARefundablePaymentTests
    {
        [TestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public void IsARefundablePayment_returns_the_expected_result(bool value, bool expectedResult)
        {
            // Arrange
            var mop = new Entities.Mop() { Metadata = new List<Entities.MopMetadata>() };
            mop.Metadata.Add(new Entities.MopMetadata() { Key = MopMetadataKeys.IsARefundablePayment, Value = value.ToString() });

            // Act
            var result = mop.IsARefundablePayment();

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void IsARefundablePayment_returns_false_when_metadata_is_null()
        {
            // Arrange
            var mop = new Entities.Mop() { Metadata = null };

            // Act
            var result = mop.IsARefundablePayment();

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsARefundablePayment_returns_false_when_metadata_is_empty()
        {
            // Arrange
            var mop = new Entities.Mop() { Metadata = new List<Entities.MopMetadata>() };

            // Act
            var result = mop.IsARefundablePayment();

            // Assert
            result.Should().BeFalse();
        }
    }
}
