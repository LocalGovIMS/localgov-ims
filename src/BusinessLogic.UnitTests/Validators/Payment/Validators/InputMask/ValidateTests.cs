using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Validators.Payment;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.Payment.Validators.InputMask
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidateTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<Func<CheckDigitType, ICheckDigitStrategy>> _mockCheckDigitStrategyFactory = new Mock<Func<CheckDigitType, ICheckDigitStrategy>>();

        private AbstractValidator _validator;

        public ValidateTests()
        {
            var mockCheckDigitStrategy = new Mock<ICheckDigitStrategy>();

            mockCheckDigitStrategy.Setup(x => x.Validate(It.IsAny<CheckDigitStrategyArgs>()));

            _mockCheckDigitStrategyFactory.Setup(x => x.Invoke(It.IsAny<CheckDigitType>()))
                .Returns(mockCheckDigitStrategy.Object);

            _validator = new InputMaskValidator(
                _mockLogger.Object,
                _mockCheckDigitStrategyFactory.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(PaymentValidationException))]
        [DataRow("$", "1")]
        [DataRow("$", "#")]
        [DataRow("$", " ")]
        [DataRow("#", "A")]
        [DataRow("#", "#")]
        [DataRow("#", " ")]
        [DataRow("?", "#")]
        [DataRow("?", " ")]
        [DataRow("*", "#")]
        [DataRow("*", "$")]
        [DataRow("*", "%")]
        [DataRow("*", "^")]
        [DataRow("*", ".")]
        [DataRow("A", "B")]
        [DataRow("A", "1")]
        [DataRow("1", "A")]
        [DataRow("1", "/")]
        public void Validate_throws_exception_when_character_is_not_of_the_expected_type(string inputMask, string reference)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(inputMask, reference));

            // Assert
        }

        [TestMethod]
        [DataRow("$", "A")]
        [DataRow("$", "B")]
        [DataRow("$", "C")]
        [DataRow("#", "1")]
        [DataRow("#", "2")]
        [DataRow("#", "3")]
        [DataRow("?", "A")]
        [DataRow("?", "1")]
        [DataRow("*", "A")]
        [DataRow("*", "1")]
        [DataRow("*", " ")]
        [DataRow("*", "/")]
        [DataRow("*", "-")]
        [DataRow("A", "A")]
        [DataRow("1", "1")]
        public void Validate_completes_when_character_is_of_the_expected_type(string inputMask, string reference)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(inputMask, reference));

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(PaymentValidationException))]
        public void Validate_throws_exception_when_check_digit_is_invalid()
        {
            // Arrange
            var mockCheckDigitStrategy = new Mock<ICheckDigitStrategy>();

            mockCheckDigitStrategy.Setup(x => x.Validate(It.IsAny<CheckDigitStrategyArgs>()))
                .Throws(new PaymentValidationException());

            _mockCheckDigitStrategyFactory.Setup(x => x.Invoke(It.IsAny<CheckDigitType>()))
                .Returns(mockCheckDigitStrategy.Object);

            // Act
            _validator.Validate(GetArgs("$@", "A1"));

            // Assert
        }

        [TestMethod]
        public void Validate_completes_when_check_digit_is_valid()
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs("$@", "A1"));

            // Assert
        }

        private PaymentValidationArgs GetArgs(string inputMask, string reference)
        {
            return new PaymentValidationArgs()
            {
                AccountReferenceValidator = new Entities.AccountReferenceValidator()
                {
                    InputMask = inputMask,
                    CheckDigitConfiguration = new Entities.CheckDigitConfiguration()
                    {
                        Type = 1
                    }
                },
                Reference = reference
            };
        }
    }
}
