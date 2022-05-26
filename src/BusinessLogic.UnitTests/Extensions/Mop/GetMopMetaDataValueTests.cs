using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.Extensions.Mop
{
    [TestClass]
    public class GetMopMetadataValueTests
    {
        private const string DefaultValue = "#12345";

        [TestMethod]
        [DataRow("#111111")]
        [DataRow("#222222")]
        public void GetMopMetadataValue_returns_the_expected_result(string value)
        {
            // Arrange
            var mop = new Entities.Mop() { Metadata = new List<Entities.MopMetadata>() };
            mop.Metadata.Add(new Entities.MopMetadata() { Key = MopMetadataKeys.TextColour, Value = value });

            // Act
            var result = mop.GetMopMetadataValue(MopMetadataKeys.TextColour);

            // Assert
            result.Should().Be(value);
        }

        [TestMethod]
        public void GetMopMetadataValue_returns_the_expected_default_value_when_metadata_is_null()
        {
            // Arrange
            var mop = new Entities.Mop() { Metadata = null };

            // Act
            var result = mop.GetMopMetadataValue(MopMetadataKeys.TextColour);

            // Assert
            result.Should().Be(string.Empty);
        }

        [TestMethod]
        public void GetMopMetadataValue_returns_the_expected_default_value_when_metadata_is_empty()
        {
            // Arrange
            var mop = new Entities.Mop() { Metadata = new List<Entities.MopMetadata>() };

            // Act
            var result = mop.GetMopMetadataValue(MopMetadataKeys.TextColour);

            // Assert
            result.Should().Be(string.Empty);
        }

        [TestMethod]
        public void GetMopMetadataValue_with_a_default_value_specified_returns_the_expected_default_value_when_metadata_is_null()
        {
            // Arrange
            var mop = new Entities.Mop() { Metadata = null };

            // Act
            var result = mop.GetMopMetadataValue(MopMetadataKeys.TextColour, DefaultValue);

            // Assert
            result.Should().Be(DefaultValue);
        }

        [TestMethod]
        public void GetMopMetadataValue_with_a_default_value_specified_returns_the_expected_default_value_when_metadata_is_empty()
        {
            // Arrange
            var mop = new Entities.Mop() { Metadata = new List<Entities.MopMetadata>() };

            // Act
            var result = mop.GetMopMetadataValue(MopMetadataKeys.TextColour, DefaultValue);

            // Assert
            result.Should().Be(DefaultValue);
        }
    }
}
