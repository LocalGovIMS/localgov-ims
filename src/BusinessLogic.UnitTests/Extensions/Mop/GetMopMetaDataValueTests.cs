using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.Extensions.Mop
{
    [TestClass]
    public class GetMopMetaDataValueTests
    {
        private const string DefaultValue = "#12345";

        [TestMethod]
        [DataRow("#111111")]
        [DataRow("#222222")]
        public void GetMopMetaDataValue_returns_the_expected_result(string value)
        {
            // Arrange
            var mop = new Entities.Mop() { MetaData = new List<Entities.MopMetaData>() };
            mop.MetaData.Add(new Entities.MopMetaData() { Key = MopMetaDataKeys.TextColour, Value = value });

            // Act
            var result = mop.GetMopMetaDataValue(MopMetaDataKeys.TextColour);

            // Assert
            result.Should().Be(value);
        }

        [TestMethod]
        public void GetMopMetaDataValue_returns_the_expected_default_value_when_metadata_is_null()
        {
            // Arrange
            var mop = new Entities.Mop() { MetaData = null };

            // Act
            var result = mop.GetMopMetaDataValue(MopMetaDataKeys.TextColour);

            // Assert
            result.Should().Be(string.Empty);
        }

        [TestMethod]
        public void GetMopMetaDataValue_returns_the_expected_default_value_when_metadata_is_empty()
        {
            // Arrange
            var mop = new Entities.Mop() { MetaData = new List<Entities.MopMetaData>() };

            // Act
            var result = mop.GetMopMetaDataValue(MopMetaDataKeys.TextColour);

            // Assert
            result.Should().Be(string.Empty);
        }

        [TestMethod]
        public void GetMopMetaDataValue_with_a_default_value_specified_returns_the_expected_default_value_when_metadata_is_null()
        {
            // Arrange
            var mop = new Entities.Mop() { MetaData = null };

            // Act
            var result = mop.GetMopMetaDataValue(MopMetaDataKeys.TextColour, DefaultValue);

            // Assert
            result.Should().Be(DefaultValue);
        }

        [TestMethod]
        public void GetMopMetaDataValue_with_a_default_value_specified_returns_the_expected_default_value_when_metadata_is_empty()
        {
            // Arrange
            var mop = new Entities.Mop() { MetaData = new List<Entities.MopMetaData>() };

            // Act
            var result = mop.GetMopMetaDataValue(MopMetaDataKeys.TextColour, DefaultValue);

            // Assert
            result.Should().Be(DefaultValue);
        }
    }
}
