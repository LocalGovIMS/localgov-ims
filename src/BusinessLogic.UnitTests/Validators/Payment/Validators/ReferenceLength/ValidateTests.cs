using BusinessLogic.Validators.Payment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.Payment.Validators.ReferenceLength
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidateTests
    {
        private AbstractValidator _validator;

        public ValidateTests()
        {
            _validator = new ReferenceLengthValidator();
        }

        [TestMethod]
        [ExpectedException(typeof(PaymentValidationException))]
        [DataRow("", 1, 5)]
        [DataRow(" ", 1, 5)]
        [DataRow("     ", 1, 5)]
        [DataRow(null, 1, 5)]
        [DataRow("123456", 1, 5)]
        [DataRow("1", 2, 5)]
        public void Validate_throws_exception_when_reference_length_is_out_of_bounds(string reference, int minLength, int maxLength)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(reference, Convert.ToInt16(minLength), Convert.ToInt16(maxLength)));

            // Assert
        }

        [TestMethod]
        [DataRow("1", 1, 5)]
        [DataRow("12", 1, 5)]
        [DataRow("123", 1, 5)]
        [DataRow("1234", 1, 5)]
        [DataRow("12345", 1, 5)]
        public void Validate_completes_when_reference_length_is_in_bounds(string reference, int minLength, int maxLength)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(reference, Convert.ToInt16(minLength), Convert.ToInt16(maxLength)));

            // Assert
        }

        private PaymentValidationArgs GetArgs(string reference, short minLength, short maxLength)
        {
            return new PaymentValidationArgs()
            {
                AccountReferenceValidator = new Entities.AccountReferenceValidator()
                {
                    MinLength = minLength,
                    MaxLength = maxLength
                },
                Reference = reference
            };
        }
    }
}
