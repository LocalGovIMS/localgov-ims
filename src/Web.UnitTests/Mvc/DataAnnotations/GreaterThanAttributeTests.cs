using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.InteropServices;
using Attribute = Web.Mvc.DataAnnotations.GreaterThanAttribute;

namespace Web.UnitTests.Mvc.DataAnnotations
{
    public interface ITestModel
    {
        decimal SomeProperty { get; set; }
        string NonDecimalProperty { get; set; }
    }

    [TestClass]
    public class GreaterThanAttributeTests
    {
        [DataRow("10", "10.001")]
        [DataRow("10", "10.01")]
        [DataRow("10", "10.1")]
        [DataRow("10", "11")]
        [TestMethod]
        public void GetValidationResult_validates_correctly(string someValue, string otherValue)
        {
            // Arrange
            var mockModel = new Mock<ITestModel>();
            mockModel.Setup(m => m.SomeProperty).Returns(Convert.ToDecimal(someValue));
            var attribute = new Attribute("SomeProperty");
            var context = new ValidationContext(mockModel.Object, null, null);

            // Act
            var result = attribute.GetValidationResult(otherValue, context);

            // Assert
            Assert.AreEqual(ValidationResult.Success, result);
        }

        [DataRow("DoesntExist")]
        [DataRow("NoIdeaWhatThisIs")]
        [DataRow("NotAnotherProperty")]
        [TestMethod]
        public void GetValidationResult_fails_validation_when_a_property_with_the_specified_name_is_not_found_on_the_model(string someProperty)
        {
            // Arrange
            var mockModel = new Mock<ITestModel>();
            mockModel.Setup(m => m.SomeProperty).Returns(10);
            var attribute = new Attribute(someProperty);
            var context = new ValidationContext(mockModel.Object, null, null);

            // Act
            var result = attribute.GetValidationResult(11, context);

            // Assert
            Assert.AreEqual($"Could not find a property named {someProperty}.", result.ErrorMessage);
        }

        [DataRow("NotANumber")]
        [DataRow("ABC")]
        [DataRow("Three")]
        [TestMethod]
        public void GetValidationResult_fails_validation_when_the_property_does_not_have_a_numeric_value(string someValue)
        {
            // Arrange
            var mockModel = new Mock<ITestModel>();
            mockModel.Setup(m => m.NonDecimalProperty).Returns(someValue);
            var attribute = new Attribute("NonDecimalProperty");
            var context = new ValidationContext(mockModel.Object, null, null);

            // Act
            var result = attribute.GetValidationResult(9, context);

            // Assert
            Assert.AreEqual($"NonDecimalProperty is not a numeric value.", result.ErrorMessage);
        }

        [TestMethod]
        public void Attribute_can_be_applied_more_than_nce()
        {
            var attributes = (IList<AttributeUsageAttribute>)typeof(Attribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false);
            Assert.AreEqual(1, attributes.Count);

            var attribute = attributes[0];
            Assert.IsTrue(attribute.AllowMultiple);
        }

        [TestMethod]
        public void Attribute_can_be_inherited()
        {
            var attributes = (IList<AttributeUsageAttribute>)typeof(Attribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false);
            Assert.AreEqual(1, attributes.Count);

            var attribute = attributes[0];
            Assert.IsTrue(attribute.Inherited);
        }

        [TestMethod]
        public void Attribute_is_only_valid_on_properties()
        {
            var attributes = (IList<AttributeUsageAttribute>)typeof(Attribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false);
            Assert.AreEqual(1, attributes.Count);

            var attribute = attributes[0];
            Assert.AreEqual(attribute.ValidOn, AttributeTargets.Property);
        }
    }
}
