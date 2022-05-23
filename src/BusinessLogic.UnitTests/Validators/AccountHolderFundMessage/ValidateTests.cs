using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.AccountHolderFundMessage
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidateTests : BaseAccountHolderFundMessageTest
    {
        private void SetupFundMessageService()
        {
            MockFundMessageService.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new Entities.FundMessage()
                    {
                        Id = 1,
                        FundCode = "F1"
                    }
                );
        }

        [TestMethod]
        public void WhenTheFundMessageIsRelatedToTheFundReturnASuccess()
        {
            // Arrange
            SetupFundMessageService();

            var validator = GetAccountHolderFundMessageValidator();
            var accountHolder = new Entities.AccountHolder() { FundCode = "F1", FundMessageId = 1 };

            // Act
            var result = validator.Validate(accountHolder);

            // Assert
            result.Success.Should().BeTrue();
        }

        [TestMethod]
        public void WhenTheFundMessageIsNotFoundReturnAFailure()
        {
            // Arrange

            var validator = GetAccountHolderFundMessageValidator();
            var accountHolder = new Entities.AccountHolder() { FundCode = "F2", FundMessageId = 3 };

            // Act
            var result = validator.Validate(accountHolder);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Should().Be("The message is not valid");
        }

        [TestMethod]
        public void WhenTheFundMessageIsNotRelatedToTheFundReturnAFailure()
        {
            // Arrange
            SetupFundMessageService();

            var validator = GetAccountHolderFundMessageValidator();
            var accountHolder = new Entities.AccountHolder() { FundCode = "F2", FundMessageId = 3 };

            // Act
            var result = validator.Validate(accountHolder);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Should().Be("The message is not valid");
        }
    }
}
