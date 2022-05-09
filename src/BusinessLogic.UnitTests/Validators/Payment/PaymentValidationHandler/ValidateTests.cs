using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using BusinessLogic.Validators.Payment;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.Payment.PaymentValidationHandler
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidateTests
    {
        private Mock<ILog> _mockLogger = new Mock<ILog>();
        private Mock<IFundService> _mockFundService = new Mock<IFundService>();
        private Mock<IAccountReferenceValidatorService> _mockAccountReferenceValidatorService = new Mock<IAccountReferenceValidatorService>();
        private Mock<ISecurityContext> _mockSecurityContext = new Mock<ISecurityContext>();
        private Mock<Func<string, IValidator<PaymentValidationArgs>>> _mockValidatorFactory = new Mock<Func<string, IValidator<PaymentValidationArgs>>>();

        private BusinessLogic.Validators.Payment.PaymentValidationHandler _handler;

        private void CreateHandler()
        {
            _mockFundService.Setup(x => x.GetByFundCode(It.IsAny<string>()))
                .Returns(new Entities.Fund());

            _mockAccountReferenceValidatorService.Setup(x => x.GetByFundCode(It.IsAny<string>()))
                .Returns(new Entities.AccountReferenceValidator());

            var mockValidator = new Mock<IValidator<PaymentValidationArgs>>();

            mockValidator.Setup(x => x.SetNext(It.IsAny<IValidator<PaymentValidationArgs>>()))
                .Returns(mockValidator.Object);

            _mockValidatorFactory.Setup(x => x.Invoke(It.IsAny<string>()))
                .Returns(mockValidator.Object);

            _handler = new BusinessLogic.Validators.Payment.PaymentValidationHandler(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockAccountReferenceValidatorService.Object,
                _mockSecurityContext.Object,
                _mockValidatorFactory.Object);
        }

        [TestMethod]
        public void Validate_returns_the_expected_result_when_the_required_fund_is_not_found()
        {
            // Arrange
            CreateHandler();

            _mockFundService.Setup(x => x.GetByFundCode(It.IsAny<string>()))
                .Returns((Entities.Fund)null);

            // Act
            var result = _handler.Validate(GetArgs());

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Should().Be($"Fund is null: { GetArgs().FundCode }");
        }

        [TestMethod]
        public void Validate_returns_the_expected_result_when_the_required_account_refernce_validator_is_not_found()
        {
            // Arrange
            CreateHandler();

            _mockAccountReferenceValidatorService.Setup(x => x.GetByFundCode(It.IsAny<string>()))
                .Returns((Entities.AccountReferenceValidator)null);

            // Act
            var result = _handler.Validate(GetArgs());

            // Assert
            result.Success.Should().BeTrue();
        }

        [TestMethod]
        public void Validate_returns_the_expected_result_when_one_of_the_validators_fails()
        {
            // Arrange
            CreateHandler();

            var mockValidator = new Mock<IValidator<PaymentValidationArgs>>();

            mockValidator.Setup(x => x.SetNext(It.IsAny<IValidator<PaymentValidationArgs>>()))
                .Returns(mockValidator.Object);

            mockValidator.Setup(x => x.Validate(It.IsAny<PaymentValidationArgs>()))
                .Throws(new PaymentValidationException("A custom payment validation exception message"));

            _mockValidatorFactory.Setup(x => x.Invoke(It.IsAny<string>()))
                .Returns(mockValidator.Object);

            // Act
            var result = _handler.Validate(GetArgs());

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Should().Be("A custom payment validation exception message");
        }

        [TestMethod]
        public void Validate_returns_the_expected_result_when_the_reference_is_valid()
        {
            // Arrange
            CreateHandler();

            // Act
            var result = _handler.Validate(GetArgs());

            // Assert
            result.Success.Should().BeTrue();
        }

        private PaymentValidationArgs GetArgs()
        {
            return new PaymentValidationArgs()
            {
                FundCode = "F1",
                Reference = "Test"
            };
        }
    }
}
