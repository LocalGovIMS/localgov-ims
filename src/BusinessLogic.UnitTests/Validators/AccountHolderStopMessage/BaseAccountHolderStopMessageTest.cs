using BusinessLogic.Interfaces.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.AccountHolderStopMessage
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseAccountHolderStopMessageTest
    {
        protected readonly Mock<IStopMessageService> MockStopMessageService = new Mock<IStopMessageService>();

        public BusinessLogic.Validators.AccountHolderStopMessageValidator GetAccountHolderStopMessageValidator()
        {
            var validator = new BusinessLogic.Validators.AccountHolderStopMessageValidator(MockStopMessageService.Object);

            return validator;
        }
    }
}
