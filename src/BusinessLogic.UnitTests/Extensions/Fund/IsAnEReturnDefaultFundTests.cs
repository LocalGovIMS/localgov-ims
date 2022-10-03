using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.Extensions.Fund
{
    [TestClass]
    public class IsAnEReturnDefaultFundTests
    {
        [TestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public void IsAnEReturnDefaultFund_returns_the_expected_result(bool value, bool expectedResult)
        {
            // Arrange
            var fund = new Entities.Fund() { Metadata = new List<Entities.FundMetadata>() };
            fund.Metadata.Add(new Entities.FundMetadata()
            {
                MetadataKey = new Entities.MetadataKey()
                {
                    Name = FundMetadataKeys.IsAnEReturnDefaultFund
                },
                Value = value.ToString()
            });

            // Act
            var result = fund.IsAnEReturnDefaultFund();

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void IsAnEReturnDefaultFund_returns_false_when_metadata_is_null()
        {
            // Arrange
            var fund = new Entities.Fund() { Metadata = null };

            // Act
            var result = fund.IsAnEReturnDefaultFund();

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsAnEReturnDefaultFund_returns_false_when_metadata_is_empty()
        {
            // Arrange
            var fund = new Entities.Fund() { Metadata = new List<Entities.FundMetadata>() };

            // Act
            var result = fund.IsAnEReturnDefaultFund();

            // Assert
            result.Should().BeFalse();
        }
    }
}
