using BusinessLogic.Enums;
using BusinessLogic.Extensions;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace BusinessLogic.UnitTests.Extensions.Mop
{
    [TestClass]
    public class BackgroundColourTests
    {
        private const string DefaultBackGroundColour = "#CCCCCC";

        [TestMethod]
        [DataRow("#FFFFFF")]
        [DataRow("#EEEEEE")]
        public void BackgroundColour_returns_the_expected_result(string value)
        {
            // Arrange
            var mop = new Entities.Mop() { Metadata = new List<Entities.MopMetadata>() };
            mop.Metadata.Add(new Entities.MopMetadata()
            {
                MetadataKey = new Entities.MetadataKey()
                {
                    Name = MopMetadataKeys.BackgroundColour
                },
                Value = value
            });

            // Act
            var result = mop.BackgroundColour();

            // Assert
            result.Should().Be(value);
        }

        [TestMethod]
        public void BackgroundColour_returns_the_expected_default_value_when_metadata_is_null()
        {
            // Arrange
            var mop = new Entities.Mop() { Metadata = null };

            // Act
            var result = mop.BackgroundColour();

            // Assert
            result.Should().Be(DefaultBackGroundColour);
        }

        [TestMethod]
        public void BackgroundColour_returns_the_expected_default_value_when_metadata_is_empty()
        {
            // Arrange
            var mop = new Entities.Mop() { Metadata = new List<Entities.MopMetadata>() };

            // Act
            var result = mop.BackgroundColour();

            // Assert
            result.Should().Be(DefaultBackGroundColour);
        }
    }
}
