using BusinessLogic.Validators.Payment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.Payment.Validators.CharacterType
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidateTests
    {
        private AbstractValidator _validator;

        public ValidateTests()
        {
            _validator = new CharacterTypeValidator();
        }

        [TestMethod]
        [ExpectedException(typeof(PaymentValidationException))]
        [DataRow("Reference1")]
        [DataRow("1Reference")]
        [DataRow("1")]
        [DataRow("_^%$^%")]
        [DataRow("Reference ")]
        [DataRow(" Reference")]
        [DataRow("Refe rence")]
        [DataRow("")]
        [DataRow(" ")]
        public void Validate_throws_exception_when_character_type_is_alpha_and_reference_is_not_alpha(string reference)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(BusinessLogic.Enums.CharacterType.Alpha, reference));

            // Assert
        }

        [TestMethod]
        [DataRow("IsAlpha")]
        [DataRow("ABCDEFG")]
        [DataRow("A")]
        public void Validate_completes_when_character_type_is_alpha_and_reference_is_alpha(string amount)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(BusinessLogic.Enums.CharacterType.Alpha, amount));

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(PaymentValidationException))]
        [DataRow("Reference1")]
        [DataRow("1Reference")]
        [DataRow("1")]
        [DataRow("_^%$^%")]
        [DataRow("1 ")]
        [DataRow(" 1")]
        [DataRow("")]
        public void Validate_throws_exception_when_character_type_is_alphawhitespace_and_reference_is_not_alphawhitespace(string reference)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(BusinessLogic.Enums.CharacterType.AlphaWhiteSpace, reference));

            // Assert
        }

        [TestMethod]
        [DataRow("IsAlpha")]
        [DataRow("ABCDEFG")]
        [DataRow("A")]
        [DataRow(" ")]
        [DataRow("A ")]
        [DataRow(" A")]
        [DataRow("A B")]
        public void Validate_completes_when_character_type_is_alphawhitespace_and_reference_is_alphawhitespace(string amount)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(BusinessLogic.Enums.CharacterType.AlphaWhiteSpace, amount));

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(PaymentValidationException))]
        [DataRow("Reference1")]
        [DataRow("1Reference")]
        [DataRow("One")]
        [DataRow("_^%$^%")]
        [DataRow(" ")]
        public void Validate_throws_exception_when_character_type_is_numeric_and_reference_is_not_numeric(string reference)
        {
            // Arrange

            // Act 
            _validator.Validate(GetArgs(BusinessLogic.Enums.CharacterType.Numeric, reference));

            // Assert
        }

        [TestMethod]
        [DataRow("1")]
        [DataRow("123456")]
        [DataRow("987564321")]
        public void Validate_completes_when_character_type_is_numeric_and_reference_is_numeric(string amount)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(BusinessLogic.Enums.CharacterType.Numeric, amount));

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(PaymentValidationException))]
        [DataRow("Reference1")]
        [DataRow("1Reference")]
        [DataRow("One")]
        [DataRow("_^%$^%")]
        [DataRow("One ")]
        [DataRow(" One")]
        [DataRow("")]
        public void Validate_throws_exception_when_character_type_is_numericwhitespace_and_reference_is_not_numericwhitespace(string reference)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(BusinessLogic.Enums.CharacterType.NumericWhiteSpace, reference));

            // Assert
        }

        [TestMethod]
        [DataRow("1")]
        [DataRow("123456")]
        [DataRow("987564321")]
        [DataRow(" ")]
        [DataRow("1 ")]
        [DataRow(" 1")]
        [DataRow("1 2")]
        public void Validate_completes_when_character_type_is_numericwhitespace_and_reference_is_numericwhitespace(string amount)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(BusinessLogic.Enums.CharacterType.NumericWhiteSpace, amount));

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(PaymentValidationException))]
        [DataRow(" ")]
        [DataRow("_^%$^%")]
        [DataRow("123456_^%$^%")]
        [DataRow("abcdef_^%$^%")]
        [DataRow("abc123_^%$^%")]
        public void Validate_throws_exception_when_character_type_is_alphanumeric_and_reference_is_not_alphanumeric(string reference)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(BusinessLogic.Enums.CharacterType.AlphaNumeric, reference));

            // Assert
        }

        [TestMethod]
        [DataRow("1")]
        [DataRow("One")]
        [DataRow("1One")]
        public void Validate_completes_when_character_type_is_alphanumeric_and_reference_is_alphanumeric(string amount)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(BusinessLogic.Enums.CharacterType.AlphaNumeric, amount));

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(PaymentValidationException))]
        [DataRow("_^%$^%")]
        [DataRow(" _^%$^%")]
        [DataRow("123456_^%$^%")]
        [DataRow("123456 _^%$^%")]
        [DataRow("abcdef_^%$^%")]
        [DataRow("abcdef _^%$^%")]
        [DataRow("abc123_^%$^%")]
        [DataRow("abc123 _^%$^%")]
        [DataRow("_^%$^% ")]
        [DataRow(" _^%$^%")]
        [DataRow("_^%$ ^%")]
        public void Validate_throws_exception_when_character_type_is_alphanumericwhitespace_and_reference_is_not_alphanumericwhitespace(string reference)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(BusinessLogic.Enums.CharacterType.AlphaNumericWhiteSpace, reference));

            // Assert
        }

        [TestMethod]
        [DataRow("1")]
        [DataRow("One")]
        [DataRow("1One")]
        [DataRow(" ")]
        public void Validate_completes_when_character_type_is_alphanumericwhitespace_and_reference_is_alphanumericwhitespace(string amount)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(BusinessLogic.Enums.CharacterType.AlphaNumericWhiteSpace, amount));

            // Assert
        }

        private PaymentValidationArgs GetArgs(BusinessLogic.Enums.CharacterType characterType, string reference)
        {
            return new PaymentValidationArgs()
            {
                AccountReferenceValidator = new Entities.AccountReferenceValidator()
                {
                    CharacterType = characterType
                },
                Reference = reference
            };
        }
    }
}
