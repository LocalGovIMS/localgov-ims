using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.Extensions.Mop
{
    [TestClass]
    public class IsAnEReturnChequePaymentTests
    {
        [TestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public void IsAnEReturnChequePayment_returns_the_expected_result(bool value, bool expectedResult)
        {
            // Arrange
            var mop = new Entities.Mop() { MetaData = new List<Entities.MopMetaData>() };
            mop.MetaData.Add(new Entities.MopMetaData() { Key = MopMetaDataKeys.IsAnEReturnChequePayment, Value = value.ToString() });

            // Act
            var result = mop.IsAnEReturnChequePayment();

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void IsAnEReturnChequePayment_returns_false_when_metadata_is_null()
        {
            // Arrange
            var mop = new Entities.Mop() { MetaData = null };

            // Act
            var result = mop.IsAnEReturnChequePayment();

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsAnEReturnChequePayment_returns_false_when_metadata_is_empty()
        {
            // Arrange
            var mop = new Entities.Mop() { MetaData = new List<Entities.MopMetaData>() };

            // Act
            var result = mop.IsAnEReturnChequePayment();

            // Assert
            result.Should().BeFalse();
        }
    }
}
