using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.AccountHolderStopMessage
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class ValidateTests : BaseAccountHolderStopMessageTest
    {
        private void SetupStopMessageService()
        {
            MockStopMessageService.Setup(x => x.GetById(It.IsAny<int>()))
                .Returns(new Entities.StopMessage()
                    {
                        Id = 1,
                        FundCode = "F1"
                    }
                );
        }

        [TestMethod]
        public void WhenTheStopMessageIsRelatedToTheFundReturnASuccess()
        {
            // Arrange
            SetupStopMessageService();

            var validator = GetAccountHolderStopMessageValidator();
            var accountHolder = new Entities.AccountHolder() { FundCode = "F1", StopMessageId = 1 };

            // Act
            var result = validator.Validate(accountHolder);

            // Assert
            result.Success.Should().BeTrue();
        }

        [TestMethod]
        public void WhenTheStopMessageIsNotFoundReturnAFailure()
        {
            // Arrange

            var validator = GetAccountHolderStopMessageValidator();
            var accountHolder = new Entities.AccountHolder() { FundCode = "F2", StopMessageId = 3 };

            // Act
            var result = validator.Validate(accountHolder);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Should().Be("The stop message is not valid");
        }

        [TestMethod]
        public void WhenTheStopMessageIsNotRelatedToTheFundReturnAFailure()
        {
            // Arrange
            SetupStopMessageService();

            var validator = GetAccountHolderStopMessageValidator();
            var accountHolder = new Entities.AccountHolder() { FundCode = "F2", StopMessageId = 3 };

            // Act
            var result = validator.Validate(accountHolder);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Should().Be("The stop message is not valid");
        }
    }
}
