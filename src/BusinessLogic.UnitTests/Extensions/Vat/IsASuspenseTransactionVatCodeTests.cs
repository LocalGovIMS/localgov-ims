﻿using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.Extensions.Mop
{
    [TestClass]
    public class IsASuspenseTransactionVatCodeTests
    {
        [TestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public void IsASuspenseTransactionVatCode_returns_the_expected_result(bool value, bool expectedResult)
        {
            // Arrange
            var vat = new Entities.Vat() { Metadata = new List<Entities.VatMetadata>() };
            vat.Metadata.Add(new Entities.VatMetadata()
            {
                MetadataKey = new Entities.MetadataKey()
                {
                    Name = VatMetadataKeys.IsASuspenseTransactionVatCode
                },
                Value = value.ToString()
            });

            // Act
            var result = vat.IsASuspenseTransactionVatCode();

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void IsASuspenseTransactionVatCode_returns_false_when_metadata_is_null()
        {
            // Arrange
            var vat = new Entities.Vat() { Metadata = null };

            // Act
            var result = vat.IsASuspenseTransactionVatCode();

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsASuspenseTransactionVatCode_returns_false_when_metadata_is_empty()
        {
            // Arrange
            var vat = new Entities.Vat() { Metadata = new List<Entities.VatMetadata>() };

            // Act
            var result = vat.IsASuspenseTransactionVatCode();

            // Assert
            result.Should().BeFalse();
        }
    }
}
