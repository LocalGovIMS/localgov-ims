﻿using BusinessLogic.Enums;
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
            var fund = new Entities.Fund() { MetaData = new List<Entities.FundMetaData>() };
            fund.MetaData.Add(new Entities.FundMetaData() { Key = FundMetaDataKeys.IsAnEReturnDefaultFund, Value = value.ToString() });

            // Act
            var result = fund.IsAnEReturnDefaultFund();

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void IsAnEReturnDefaultFund_returns_false_when_metadata_is_null()
        {
            // Arrange
            var fund = new Entities.Fund() { MetaData = null };

            // Act
            var result = fund.IsAnEReturnDefaultFund();

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsAnEReturnDefaultFund_returns_false_when_metadata_is_empty()
        {
            // Arrange
            var fund = new Entities.Fund() { MetaData = new List<Entities.FundMetaData>() };

            // Act
            var result = fund.IsAnEReturnDefaultFund();

            // Assert
            result.Should().BeFalse();
        }
    }
}
