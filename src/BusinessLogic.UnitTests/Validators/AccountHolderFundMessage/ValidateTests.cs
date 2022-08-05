using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.AccountHolderFundMessage
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidateTests : BaseAccountHolderFundMessageTest
    {
        private const int ValidFundMessageId = 1;
        private const int InvalidFundMessageId = 2;

        private const string ValidFundCode = "F1";
        private const string InvalidFundCode = "F2";

        private void SetupFundMessageService()
        {
            MockFundMessageService.Setup(x => x.GetAll())
                .Returns(new List<Entities.FundMessage>()
                    {
                        new Entities.FundMessage() {
                            Id = ValidFundMessageId,
                            FundCode = ValidFundCode
                        }
                    }
                );
        }

        [TestMethod]
        public void WhenTheFundMessageIsRelatedToTheFundReturnASuccess()
        {
            // Arrange
            SetupFundMessageService();

            var validator = GetAccountHolderFundMessageValidator();
            var accountHolder = new Entities.AccountHolder() { FundCode = ValidFundCode, FundMessageId = ValidFundMessageId };

            // Act
            var result = validator.Validate(accountHolder);

            // Assert
            result.Success.Should().BeTrue();
        }

        [TestMethod]
        public void WhenTheFundMessageIsNotFoundReturnAFailure()
        {
            // Arrange
            SetupFundMessageService();

            var validator = GetAccountHolderFundMessageValidator();
            var accountHolder = new Entities.AccountHolder() { FundCode = ValidFundCode, FundMessageId = InvalidFundMessageId };

            // Act
            var result = validator.Validate(accountHolder);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Should().Be("The fund message is not valid");
        }

        [TestMethod]
        public void WhenTheFundMessageIsNotRelatedToTheFundReturnAFailure()
        {
            // Arrange
            SetupFundMessageService();

            var validator = GetAccountHolderFundMessageValidator();
            var accountHolder = new Entities.AccountHolder() { FundCode = InvalidFundCode, FundMessageId = ValidFundMessageId };

            // Act
            var result = validator.Validate(accountHolder);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Should().Be("The fund message is not valid for the fund code specified");
        }
    }
}
