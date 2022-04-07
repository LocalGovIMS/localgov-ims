using BusinessLogic.Validators.Payment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.Payment.Validators.Regex
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidateTests
    {
        private AbstractValidator _validator;

        public ValidateTests()
        {
            _validator = new RegexValidator();
        }

        [TestMethod]
        [ExpectedException(typeof(PaymentValidationException))]
        [DataRow("AB12345!")]
        [DataRow("AB12345C")]
        [DataRow("AB12345")]
        [DataRow("AA123456")]
        [DataRow("AA12345Y")]
        [DataRow("AA12345Z")]
        [DataRow("ABB2345Z")]
        public void Validate_throws_exception_when_no_regex_matches_are_identified(string reference)
        {
            // Arrange
            
            // Act
            _validator.Validate(GetArgs(@"^AB\d{5}(\d|Y|Z)$", reference));

            // Assert
        }

        [TestMethod]
        [DataRow("AB123456")]
        [DataRow("AB12345Y")]
        [DataRow("AB12345Z")]
        [DataRow("AB000000")]
        [DataRow("AB00000Y")]
        [DataRow("AB00000Z")]
        public void Validate_completes_when_regex_matches_are_identified(string reference)
        {
            // Arrange
            
            // Act
            _validator.Validate(GetArgs(@"^AB\d{5}(\d|Y|Z)$", reference));

            // Assert
        }

        [TestMethod]
        [DataRow("")]
        [DataRow(" ")]
        [DataRow(null)]
        public void Validate_completes_when_no_regex_is_spcified(string regex)
        {
            // Arrange

            // Act
            _validator.Validate(GetArgs(regex, "Ref"));
            
            // Assert
        }

        private PaymentValidationArgs GetArgs(string regex, string reference)
        {
            return new PaymentValidationArgs()
            {
                AccountReferenceValidator = new Entities.AccountReferenceValidator()
                {
                    Regex = regex,
                },
                Reference = reference
            };
        }
    }
}
