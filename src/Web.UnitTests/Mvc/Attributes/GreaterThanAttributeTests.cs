using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;
using Web.Mvc.DataAnnotations;

namespace Web.UnitTests.Mvc.Attributes
{
    [TestClass]
    public class GreaterThanAttributeTests
    {
        [TestMethod]
        public void Constructor_throws_an_exception_when_other_property_is_null()
        {
            // Arrange
            var target = new NullOtherProperty() { Min = 10, Max = 20 };

            var context = new ValidationContext(target);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(target, context, results, true);

            // Assert
            isValid.Should().Be(false);
        }

        [TestMethod]
        [DataRow(9, false)]
        [DataRow(10, false)]
        [DataRow(11, true)]
        public void IsValid_returns_the_expected_value_when_allow_equals_is_false(int max, bool expectedResult)
        {
            // Arrange
            var target = new GreaterThanTarget() { Min = 10, Max = max };
            
            var context = new ValidationContext(target);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(target, context, results, true);

            // Assert
            isValid.Should().Be(expectedResult);
        }

        [TestMethod]
        [DataRow(9, false)]
        [DataRow(10, true)]
        [DataRow(11, true)]
        public void IsValid_returns_the_expected_value_when_allow_equals_is_true(int max, bool expectedResult)
        {
            // Arrange
            var target = new GreaterThanOrEqualToTarget() { Min = 10, Max = max };

            var context = new ValidationContext(target);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(target, context, results, true);

            // Assert
            isValid.Should().Be(expectedResult);
        }

        [TestMethod]
        [DataRow("9", false)]
        [DataRow("10", true)]
        [DataRow("10.01", true)]
        [DataRow("10X", false)]
        [DataRow("Ten", false)]
        [DataRow("%$£", false)]
        public void IsValid_returns_the_expected_result_when_the_value_provided_is_not_a_decimal(string value, bool expectedResult)
        {
            // Arrange
            var target = new InvalidDataTypeTarget() { Min = 10, Max = value };

            var context = new ValidationContext(target);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(target, context, results, true);

            // Assert
            isValid.Should().Be(expectedResult);
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

        [DataRow("AProperty", "*.AProperty")]
        [DataRow("AnotherProperty", "*.AnotherProperty")]
        [TestMethod]
        public void FormatPropertyForClientValidation_returns_a_correctly_formatted_value(string property, string expectedResult)
        {
            // Arrange

            // Act
            var result = GreaterThanAttribute.FormatPropertyForClientValidation(property);

            // Assert
            result.Should().Be(expectedResult);
        }

        [TestMethod]
        public void FormatPropertyForClientValidation_throws_an_ArgumentException_if_the_value_provided_is_null()
        {
            // Arrange

            // Act
            Action act = () => GreaterThanAttribute.FormatPropertyForClientValidation(null);

            // Assert
            act.Should()
                .Throw<ArgumentException>()
                .WithMessage("Value cannot be null or empty.\r\nParameter name: property");
        }

        private class NullOtherProperty
        {
            public int Min { get; set; }

            [GreaterThan(null, AllowEquality = true)]
            public int Max { get; set; }
        }

        private class GreaterThanTarget
        {
            public int Min { get; set; }

            [GreaterThan(nameof(Min), AllowEquality = false)]
            public int Max { get; set; }
        }

        private class GreaterThanOrEqualToTarget
        {
            public int Min { get; set; }

            [GreaterThan(nameof(Min), AllowEquality = true)]
            public int Max { get; set; }
        }

        private class InvalidDataTypeTarget
        {
            public int Min { get; set; }

            [GreaterThan(nameof(Min), AllowEquality = true)]
            public string Max { get; set; }
        }
    }
}
