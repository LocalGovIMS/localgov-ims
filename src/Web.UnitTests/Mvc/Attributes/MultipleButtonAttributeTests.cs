using Admin.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Web.Mvc;

namespace Web.UnitTests.Mvc.Attributes
{
    [TestClass]
    public class MultipleButtonAttributeTests
    {

        [DataRow("Test", true)]
        [DataRow("Test1", false)]
        [TestMethod]
        public void IsValidName_validates_correctly(string testValue, bool expectedResult)
        {
            // Arrange
            var attribute = new MultipleButtonAttribute()
            {
                MatchFormKey = "action",
                MatchFormValue = "Test"
            };

            var request = new Mock<HttpRequestBase>();
            request.SetupGet(x => x["action"]).Returns(testValue);

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            var _controllerInstance = new HomeController(new Mock<IHomeControllerDependencies>().Object);
            var controllerContext = new ControllerContext(context.Object, new RouteData(), _controllerInstance);

            // Act
            var result = attribute.IsValidName(controllerContext, "action", null);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        [DataRow("Test", false)]
        [DataRow("Test1", false)]
        [TestMethod]
        public void IsValidName_is_false_when_the_action_is_null(string testValue, bool expectedResult)
        {
            // Arrange
            var attribute = new MultipleButtonAttribute()
            {
                MatchFormKey = "action",
                MatchFormValue = "Test"
            };

            var request = new Mock<HttpRequestBase>();

            var context = new Mock<HttpContextBase>();
            context.SetupGet(x => x.Request).Returns(request.Object);

            var _controllerInstance = new HomeController(new Mock<IHomeControllerDependencies>().Object);
            var controllerContext = new ControllerContext(context.Object, new RouteData(), _controllerInstance);

            // Act
            var result = attribute.IsValidName(controllerContext, "action", null);

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        [TestMethod]
        public void Attribute_can_only_be_applied_once()
        {
            var attributes = (IList<AttributeUsageAttribute>)typeof(MultipleButtonAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false);
            Assert.AreEqual(1, attributes.Count);

            var attribute = attributes[0];
            Assert.IsFalse(attribute.AllowMultiple);
        }

        [TestMethod]
        public void Attribute_can_be_inherited()
        {
            var attributes = (IList<AttributeUsageAttribute>)typeof(MultipleButtonAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false);
            Assert.AreEqual(1, attributes.Count);

            var attribute = attributes[0];
            Assert.IsTrue(attribute.Inherited);
        }

        [TestMethod]
        public void Attribute_is_only_valid_on_methods()
        {
            var attributes = (IList<AttributeUsageAttribute>)typeof(MultipleButtonAttribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false);
            Assert.AreEqual(1, attributes.Count);

            var attribute = attributes[0];
            Assert.AreEqual(attribute.ValidOn, AttributeTargets.Method);
        }
    }
}
