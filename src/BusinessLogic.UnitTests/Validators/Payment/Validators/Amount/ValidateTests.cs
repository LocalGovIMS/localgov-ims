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

        private decimal FundMaxAmount = 99.99m;
        private decimal MopMaxAmount = 98.99m;
        private decimal MopMinAmount = 97.99m;

        public ValidateTests()
        {
            _validator = new AmountValidator();
        }

        [TestMethod]
        [ExpectedException(typeof(PaymentValidationException))]
        [DataRow("100")]
        [DataRow("100.00")]
        [DataRow("99.999")]
        public void Validate_throws_exception_when_amount_is_greater_than_the_fund_maximum_amount(string amount)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(amount));

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(PaymentValidationException))]
        [DataRow("99.99")]
        [DataRow("99")]
        [DataRow("98.999")]
        public void Validate_throws_exception_when_amount_is_greater_than_the_mop_maximum_amount(string amount)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(amount));

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(PaymentValidationException))]
        [DataRow("97.98")]
        [DataRow("97")]
        [DataRow("97.989")]
        public void Validate_throws_exception_when_amount_is_less_than_the_mop_minimum_amount(string amount)
        { 
            // Arrange

            // Act
            _validator.Validate(GetArgs(amount));

            // Assert
        }

        [TestMethod]
        [DataRow("98.98")]
        [DataRow("97.999")]
        [DataRow("98")]
        public void Validate_completes_when_amount_within_an_acceptable_range(string amount)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(amount));

            // Assert
        }

        private PaymentValidationArgs GetArgs(string amount)
        {
            return new PaymentValidationArgs()
            {
                Fund = new Entities.Fund()
                {
                    MaximumAmount = FundMaxAmount
                },
                Mop = new Entities.Mop()
                {
                    MinimumAmount = MopMinAmount,
                    MaximumAmount = MopMaxAmount
                },
                Amount = Convert.ToDecimal(amount)
            };
        }
    }
}
