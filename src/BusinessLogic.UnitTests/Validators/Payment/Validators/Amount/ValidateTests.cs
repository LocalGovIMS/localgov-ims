using BusinessLogic.Validators.Payment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.Payment.Validators.Amount
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidateTests
    {
        private AbstractValidator _validator;

        public ValidateTests()
        {
            _validator = new AmountValidator();
        }

        [TestMethod]
        [ExpectedException(typeof(PaymentValidationException))]
        [DataRow("99.99", "100")]
        [DataRow("99.99", "100.00")]
        [DataRow("99.99", "99.999")]
        public void Validate_throws_exception_when_amount_is_greater_than_the_maximum_amount(string maximumAmount, string amount)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(maximumAmount, amount));

            // Assert
        }

        [TestMethod]
        [DataRow("100", "100.00")]
        [DataRow("100", "99.99")]
        [DataRow("100.00", "100")]
        public void Validate_completes_when_amount_is_less_than_or_equal_to_the_maximum_amount(string maximumAmount, string amount)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(maximumAmount, amount));

            // Assert
        }

        private PaymentValidationArgs GetArgs(string maximumAmount, string amount)
        {
            return new PaymentValidationArgs()
            {
                Fund = new Entities.Fund()
                {
                    MaximumAmount = string.IsNullOrWhiteSpace(maximumAmount) ? (decimal?)null : Convert.ToDecimal(maximumAmount)
                },
                Amount = Convert.ToDecimal(amount)
            };
        }
    }
}
