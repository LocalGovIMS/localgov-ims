using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.Extensions.Mop
{
    [TestClass]
    public class TextColourTests
    {
        private const string DefaultTextColour = "#FFFFFF";

        [TestMethod]
        [DataRow("#111111")]
        [DataRow("#222222")]
        public void TextColour_returns_the_expected_result(string value)
        {
            // Arrange
            var mop = new Entities.Mop() { Metadata = new List<Entities.MopMetadata>() };
            mop.Metadata.Add(new Entities.MopMetadata()
            {
                MetadataKey = new Entities.MetadataKey()
                {
                    Name = MopMetadataKeys.TextColour
                },
                Value = value
            });

            // Act
            var result = mop.TextColour();

            // Assert
            result.Should().Be(value);
        }

        [TestMethod]
        public void TextColour_returns_the_expected_default_value_when_metadata_is_null()
        {
            // Arrange
            var mop = new Entities.Mop() { Metadata = null };

            // Act
            var result = mop.TextColour();

            // Assert
            result.Should().Be(DefaultTextColour);
        }

        [TestMethod]
        public void TextColour_returns_the_expected_default_value_when_metadata_is_empty()
        {
            // Arrange
            var mop = new Entities.Mop() { Metadata = new List<Entities.MopMetadata>() };

            // Act
            var result = mop.TextColour();

            // Assert
            result.Should().Be(DefaultTextColour);
        }
    }
}
