using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Validators.Payment;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.Payment.Validators.AccountExists
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidateTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<ISecurityContext> _mockSecurityContext = new Mock<ISecurityContext>();
        private readonly Mock<IAccountHolderService> _mockAccountHolderService = new Mock<IAccountHolderService>();

        private AbstractValidator _validator;

        public ValidateTests()
        {
            _mockAccountHolderService.Setup(x => x.GetByAccountReference(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Entities.AccountHolder());

            _validator = new AccountExistsValidator(
                _mockLogger.Object,
                _mockSecurityContext.Object,
                _mockAccountHolderService.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(PaymentValidationException))]
        public void Validate_throws_exception_when_account_holder_does_not_exist()
        {
            // Arrange
            _mockAccountHolderService.Setup(x => x.GetByAccountReference(It.IsAny<string>(), It.IsAny<string>()))
                .Returns((Entities.AccountHolder)null);

            // Act
            _validator.Validate(GetArgs());

            // Assert
        }

        [TestMethod]
        [ExpectedException(typeof(PaymentValidationException))]
        public void Validate_throws_exception_when_account_holder_is_on_stop_and_user_is_not_a_finance_user()
        {
            // Arrange
            PutAccountHolderOnStop();
            MakeUserAFinanceUser(false);

            // Act
            _validator.Validate(GetArgs());

            // Assert
        }

        [TestMethod]
        public void Validate_completes_when_account_holder_is_on_stop_and_user_is_a_finance_user()
        {
            // Arrange
            PutAccountHolderOnStop();
            MakeUserAFinanceUser(true);

            // Act
            _validator.Validate(GetArgs());

            // Assert
        }

        [TestMethod]
        public void Validate_completes_when_account_holder_is_not_on_stop_and_user_is_not_a_finance_user()
        {
            // Arrange
            MakeUserAFinanceUser(false);

            // Act
            _validator.Validate(GetArgs());

            // Assert
        }

        [TestMethod]
        public void Validate_completes_when_account_holder_is_not_on_stop_and_user_is_a_finance_user()
        {
            // Arrange
            MakeUserAFinanceUser(true);

            // Act
            _validator.Validate(GetArgs());

            // Assert
        }
               
        private void PutAccountHolderOnStop()
        {
            _mockAccountHolderService.Setup(x => x.GetByAccountReference(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(new Entities.AccountHolder() { FundMessageId = 1 });
        }

        private void MakeUserAFinanceUser(bool isFinanceUser)
        {
            _mockSecurityContext.Setup(x => x.IsInRole(BusinessLogic.Security.Role.Finance))
                .Returns(isFinanceUser);
        }

        private PaymentValidationArgs GetArgs()
        {
            return new PaymentValidationArgs()
            {
                Fund = new Entities.Fund()
                {
                    FundCode = "F1"
                },
                Reference = "Reference"
            };
        }
    }
}
