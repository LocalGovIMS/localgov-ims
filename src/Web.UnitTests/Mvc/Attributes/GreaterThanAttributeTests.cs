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
    }

    
}
