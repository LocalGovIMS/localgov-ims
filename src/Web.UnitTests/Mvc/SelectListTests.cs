using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace Web.UnitTests.Mvc
{
    [TestClass]
    public class SelectListTests
    {
        private Web.Mvc.SelectList _selectList;

        public SelectListTests()
        {
            var items = new List<Web.Mvc.SelectListItem>()
            {
                new Web.Mvc.SelectListItem() { Text = "Text", Value = "1" }
            };

            _selectList = new Web.Mvc.SelectList() { Items = items };
        }

        [TestMethod]
        public void ToSelectList_returns_the_id_in_the_Text_property_of_each_item_when_includeIdInText_parameter_is_true()
        {
            // Arrange

            // Act
            var result = _selectList.ToSelectList(true);

            // Assert
            result[0].Text.Should().Be("Text - (1)");
        }

        [TestMethod]
        public void ToSelectList_returns_just_the_Text_property_of_each_item_when_includeIdInText_parameter_is_false()
        {
            // Arrange

            // Act
            var result = _selectList.ToSelectList(false);

            // Assert
            result[0].Text.Should().Be("Text");
        }
    }
}
