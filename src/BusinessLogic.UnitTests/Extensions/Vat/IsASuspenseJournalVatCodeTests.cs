using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.Extensions.Mop
{
    [TestClass]
    public class IsASuspenseJournalVatCodeTests
    {
        [TestMethod]
        [DataRow(true, true)]
        [DataRow(false, false)]
        public void IsASuspenseJournalVatCode_returns_the_expected_result(bool value, bool expectedResult)
        {
            // Arrange
            var vat = new Entities.Vat() { MetaData = new List<Entities.VatMetaData>() };
            vat.MetaData.Add(new Entities.VatMetaData() { Key = VatMetaDataKeys.IsASuspenseJournalVatCode, Value = value.ToString() });

            // Act
            var result = vat.IsASuspenseJournalVatCode();

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void IsASuspenseJournalVatCode_returns_false_when_metadata_is_null()
        {
            // Arrange
            var vat = new Entities.Vat() { MetaData = null };

            // Act
            var result = vat.IsASuspenseJournalVatCode();

            // Assert
            result.Should().BeFalse();
        }

        [TestMethod]
        public void IsASuspenseJournalVatCode_returns_false_when_metadata_is_empty()
        {
            // Arrange
            var vat = new Entities.Vat() { MetaData = new List<Entities.VatMetaData>() };

            // Act
            var result = vat.IsASuspenseJournalVatCode();

            // Assert
            result.Should().BeFalse();
        }
    }
}
