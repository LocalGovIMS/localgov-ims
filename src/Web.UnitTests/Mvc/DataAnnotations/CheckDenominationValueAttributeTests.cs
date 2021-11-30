using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Attribute = Web.Mvc.DataAnnotations.CheckDenominationValueAttribute;

namespace Web.UnitTests.Mvc.DataAnnotations
{
    [TestClass]
    public class CheckDenominationValueAttributeTests
    {
        [DataRow(00.01, "00.005", false)]
        [DataRow(00.01, "00.01", true)]
        [DataRow(00.01, "00.02", true)]
        [DataRow(00.02, "00.01", false)]
        [DataRow(00.02, "00.02", true)]
        [DataRow(00.02, "00.03", false)]
        [DataRow(00.02, "00.04", true)]
        [DataRow(00.05, "00.04", false)]
        [DataRow(00.05, "00.05", true)]
        [DataRow(00.05, "00.06", false)]
        [DataRow(00.05, "00.10", true)]
        [DataRow(00.10, "00.09", false)]
        [DataRow(00.10, "00.10", true)]
        [DataRow(00.10, "00.11", false)]
        [DataRow(00.10, "00.20", true)]
        [DataRow(00.20, "00.19", false)]
        [DataRow(00.20, "00.20", true)]
        [DataRow(00.20, "00.21", false)]
        [DataRow(00.20, "00.40", true)]
        [DataRow(00.50, "00.49", false)]
        [DataRow(00.50, "00.50", true)]
        [DataRow(00.50, "00.51", false)]
        [DataRow(00.50, "01.00", true)]
        [DataRow(01.00, "00.99", false)]
        [DataRow(01.00, "01.00", true)]
        [DataRow(01.00, "01.01", false)]
        [DataRow(01.00, "02.00", true)]
        [DataRow(02.00, "01.99", false)]
        [DataRow(02.00, "02.00", true)]
        [DataRow(02.00, "02.01", false)]
        [DataRow(02.00, "04.00", true)]
        [DataRow(05.00, "04.99", false)]
        [DataRow(05.00, "05.00", true)]
        [DataRow(05.00, "05.01", false)]
        [DataRow(05.00, "10.00", true)]
        [DataRow(10.00, "09.99", false)]
        [DataRow(10.00, "10.00", true)]
        [DataRow(10.00, "10.01", false)]
        [DataRow(10.00, "20.00", true)]
        [DataRow(20.00, "19.99", false)]
        [DataRow(20.00, "20.00", true)]
        [DataRow(20.00, "20.01", false)]
        [DataRow(50.00, "49.99", false)]
        [DataRow(50.00, "50.00", true)]
        [DataRow(50.00, "50.01", false)]
        [DataRow(50.00, "100.00", true)]
        [DataRow(20.00, "40.00", true)]
        [TestMethod]
        public void GetValidationResult_validates_correctly(double denomination, string value, bool expectedResult)
        {
            // Arrange
            var validationContext = new ValidationContext("") { DisplayName = "A Name" };
            var attribute = new Attribute(denomination);

            // Act
            var result = (ValidationResult.Success == attribute.GetValidationResult(Convert.ToDecimal(value), validationContext));

            // Assert
            Assert.AreEqual(result, expectedResult);
        }

        [TestMethod]
        public void Attribute_can_only_be_applied_once()
        {
            var attributes = (IList<AttributeUsageAttribute>)typeof(Attribute).GetCustomAttributes(typeof(AttributeUsageAttribute), false);
            Assert.AreEqual(1, attributes.Count);

            var attribute = attributes[0];
            Assert.IsFalse(attribute.AllowMultiple);
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
