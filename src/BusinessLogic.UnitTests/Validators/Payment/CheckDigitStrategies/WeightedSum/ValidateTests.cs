using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Validators.Payment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.Payment.CheckDigitStrategies.WeightedSum
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidateTests
    {
        private ICheckDigitStrategy _strategy;

        public ValidateTests()
        {
            _strategy = new WeightedSumStrategy();
        }

        [TestMethod]
        [ExpectedException(typeof(CheckDigitValidationException))]
        [DynamicData(nameof(TestHelpers.InvalidTestData), typeof(TestHelpers), DynamicDataSourceType.Property)]
        public void Validate_throws_exception_when_no_weightings_are_specified(Entities.CheckDigitConfiguration checkDigitConfiguration,
            string reference,
            char checkDigit)
        {
            // Arrange
            checkDigitConfiguration.Weightings = string.Empty;

            // Act
            _strategy.Validate(GetArgs(checkDigitConfiguration, reference, checkDigit));

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(CheckDigitValidationException))]
        [DynamicData(nameof(TestHelpers.InvalidTestData), typeof(TestHelpers), DynamicDataSourceType.Property)]
        public void Validate_throws_exception_when_the_check_digit_is_invalid(
            Entities.CheckDigitConfiguration checkDigitConfiguration,
            string reference,
            char checkDigit)
        {
            // Arrange

            // Act
            _strategy.Validate(GetArgs(checkDigitConfiguration, reference, checkDigit));

            // Assert
        }

        [TestMethod]
        [DynamicData(nameof(TestHelpers.ValidTestData), typeof(TestHelpers), DynamicDataSourceType.Property)]
        public void Validate_completes_when_the_check_digit_is_valid(
            Entities.CheckDigitConfiguration checkDigitConfiguration,
            string reference,
            char checkDigit)
        {
            // Arrange

            // Act
            _strategy.Validate(GetArgs(checkDigitConfiguration, reference, checkDigit));

            // Assert
        }

        private CheckDigitStrategyArgs GetArgs(
            Entities.CheckDigitConfiguration checkDigitConfiguration,
            string reference,
            char checkDigit)
        {
            return new CheckDigitStrategyArgs()
            {
                CheckDigitConfiguration = checkDigitConfiguration,
                Reference = reference,
                CheckDigit = checkDigit
            };
        }
    }
}
