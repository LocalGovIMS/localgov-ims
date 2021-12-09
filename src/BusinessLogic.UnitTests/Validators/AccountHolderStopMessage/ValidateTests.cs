using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
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
            MockStopMessageService.Setup(x => x.GetAll())
                .Returns(new List<Entities.StopMessage>()
                {
                    new Entities.StopMessage()
                    {
                        Id = "1",
                        FundCode = "F1"
                    },
                    new Entities.StopMessage()
                    {
                        Id = "2",
                        FundCode = "F2"
                    },
                    new Entities.StopMessage()
                    {
                        Id = "3",
                        FundCode = "F2"
                    },
                });
        }

        [TestMethod]
        public void WhenTheStopMessageIsRelatedToTheFundReturnASuccess()
        {
            // Arrange
            SetupStopMessageService();

            var validator = GetAccountHolderStopMessageValidator();
            var accountHolder = new Entities.AccountHolder() { FundCode = "F1", StopMessageReference = "1" };

            // Act
            var result = validator.Validate(accountHolder);

            // Assert
            result.Success.Should().BeTrue();
        }

        [TestMethod]
        public void WhenTheStopMessageIsNotRelatedToTheFundReturnAFailure()
        {
            // Arrange
            SetupStopMessageService();

            var validator = GetAccountHolderStopMessageValidator();
            var accountHolder = new Entities.AccountHolder() { FundCode = "F2", StopMessageReference = "1" };

            // Act
            var result = validator.Validate(accountHolder);

            // Assert
            result.Success.Should().BeFalse();
            result.Error.Should().Be("The fund/stop message combination is not valid");
        }
    }
}
