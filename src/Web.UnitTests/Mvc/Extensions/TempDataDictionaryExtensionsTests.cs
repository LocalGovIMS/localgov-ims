using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Web.Mvc.Extensions;

namespace Web.UnitTests.Mvc.Extensions
{
    [TestClass]
    public class TempDataDictionaryExtensionsTests
    {
        [TestMethod]
        public void AddOrUpdate_adds_a_new_value_to_the_dictionary()
        {
            // Arrange
            var tempData = new TempDataDictionary();

            // Act
            tempData.AddOrUpdate("key", "value");

            // Assert
            tempData.ContainsKey("key").Should().BeTrue();
        }

        [TestMethod]
        public void AddOrUpdate_updates_an_existing_value_in_the_dictionary()
        {
            // Arrange
            var tempData = new TempDataDictionary();
            tempData.AddOrUpdate("key", "value");

            // Act
            tempData.AddOrUpdate("key", "new value");

            // Assert
            tempData.ContainsKey("key").Should().BeTrue();
            tempData["key"].Should().Be("new value");
        }

        [TestMethod]
        public void AddOrUpdate_does_not_add_a_vaue_if_it_is_null_and_the_addIfNull_value_is_false()
        {
            // Arrange
            var tempData = new TempDataDictionary();

            // Act
            tempData.AddOrUpdate("key", null);

            // Assert
            tempData.ContainsKey("key").Should().BeFalse();
        }

        [TestMethod]
        public void AddOrUpdate_adds_a_vaue_if_it_is_null_and_the_addIfNull_value_is_true()
        {
            // Arrange
            var tempData = new TempDataDictionary();

            // Act
            tempData.AddOrUpdate("key", null, true);

            // Assert
            tempData.ContainsKey("key").Should().BeTrue();
        }
    }
}
