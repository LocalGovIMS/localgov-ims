using BusinessLogic.Classes.Result;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.TemplateReference
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TemplateReferenceValidatorTests
    {
        private Mock<ILog> _mockLogger = new Mock<ILog>();

        private BusinessLogic.Validators.TemplateRowValidator GetValidator()
        {
            return new BusinessLogic.Validators.TemplateRowValidator(_mockLogger.Object);
        }

        [TestMethod]
        public void CanInstatiate()
        {
            try
            {
                var validator = GetValidator();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void MissingRowReturnsFailResult()
        {
            // Arrange
            var validator = GetValidator();

            // Act
            var result = validator.Validate(null);

            // Assert
            result.Should().BeOfType(expectedType: typeof(Result));
            result.Success.Should().BeFalse();
            result.Error.Should().Be("There is no template row to validate");
        }

        [TestMethod]
        [DataRow("", "Description", "VatCode", "The reference is missing")]
        [DataRow("Reference", "", "VatCode", "The description is missing")]
        [DataRow("Reference", "Description", "", "The VAT code is missing")]
        [DataRow("1234567890", "Description", "VatCode", "The reference should be 11 characters long")]
        public void FieldExistenceAndLengthChecks(string reference, string description, string vatCode, string error)
        {
            // Arrange
            var validator = GetValidator();

            // Act
            var result = validator.Validate(new Entities.TemplateRow()
            {
                Reference = reference,
                Description = description,
                VatCode = vatCode
            });

            // Assert
            result.Should().BeOfType(expectedType: typeof(Result));
            result.Success.Should().BeFalse();
            result.Error.Should().Be(error);
        }

        [TestMethod]
        [DataRow("1234567890*")]
        [DataRow("1234567890-")]
        [DataRow("***********")]
        [DataRow("ABCDEFGHIJK")]
        public void NonOverridableReferenceFailureChecks(string reference)
        {
            // Arrange
            var validator = GetValidator();

            // Act
            var result = validator.Validate(new Entities.TemplateRow()
            {
                Reference = reference,
                Description = "Description",
                VatCode = "VatCode",
                ReferenceOverride = false
            });

            // Assert
            result.Should().BeOfType(expectedType: typeof(Result));
            result.Success.Should().BeFalse();
            result.Error.Should().Be(string.Format("{0} is not a valid reference - it must only contain digits", reference));
        }

        [TestMethod]
        [DataRow("12345678901")]
        [DataRow("76576537653")]
        [DataRow("65476765765")]
        [DataRow("76765765765")]
        public void NonOverridableReferenceSuccessChecks(string reference)
        {
            // Arrange
            var validator = GetValidator();

            // Act
            var result = validator.Validate(new Entities.TemplateRow()
            {
                Reference = reference,
                Description = "Description",
                VatCode = "VatCode",
                ReferenceOverride = false
            });

            // Assert
            result.Should().BeOfType(expectedType: typeof(Result));
            result.Success.Should().BeTrue();
        }

        [TestMethod]
        [DataRow("123456789.*")]
        [DataRow("12******-==")]
        [DataRow("12345A78901")]
        [DataRow("ABCDEFGHIJK")]
        public void OverridableReferenceFailureChecks(string reference)
        {
            // Arrange
            var validator = GetValidator();

            // Act
            var result = validator.Validate(new Entities.TemplateRow()
            {
                Reference = reference,
                Description = "Description",
                VatCode = "VatCode",
                ReferenceOverride = true
            });

            // Assert
            result.Should().BeOfType(expectedType: typeof(Result));
            result.Success.Should().BeFalse();
            result.Error.Should().Be(string.Format("{0} is not a valid reference - it must only contain digits and asterisks", reference));
        }

        [TestMethod]
        [DataRow("12345678901")]
        [DataRow("7657653765*")]
        [DataRow("65476******")]
        [DataRow("***********")]
        public void OverridableReferenceSuccessChecks(string reference)
        {
            // Arrange
            var validator = GetValidator();

            // Act
            var result = validator.Validate(new Entities.TemplateRow()
            {
                Reference = reference,
                Description = "Description",
                VatCode = "VatCode",
                ReferenceOverride = true
            });

            // Assert
            result.Should().BeOfType(expectedType: typeof(Result));
            result.Success.Should().BeTrue();
        }
    }
}
