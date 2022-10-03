using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.Extensions.Fund
{
    [TestClass]
    public class IsASuspenseJournalFundTests
    {
        [TestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public void IsASuspenseJournalFund_returns_the_expected_result(bool value, bool expectedResult)
        {
            // Arrange
            var fund = new Entities.Fund() { Metadata = new List<Entities.FundMetadata>() };
            fund.Metadata.Add(new Entities.FundMetadata()
            {
                MetadataKey = new Entities.MetadataKey()
                {
                    Name = FundMetadataKeys.IsASuspenseJournalFund
                },
                Value = value.ToString()
            });

            // Act
            var result = fund.IsASuspenseJournalFund();

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void IsASuspenseJournalFund_returns_false_when_metadata_is_null()
        {
            // Arrange
            var fund = new Entities.Fund() { Metadata = null };

            // Act
            var result = fund.IsASuspenseJournalFund();

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsASuspenseJournalFund_returns_false_when_metadata_is_empty()
        {
            // Arrange
            var fund = new Entities.Fund() { Metadata = new List<Entities.FundMetadata>() };

            // Act
            var result = fund.IsASuspenseJournalFund();

            // Assert
            result.Should().BeFalse();
        }
    }
}
