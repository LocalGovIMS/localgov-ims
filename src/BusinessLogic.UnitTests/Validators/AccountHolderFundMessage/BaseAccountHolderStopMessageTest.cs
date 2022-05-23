using BusinessLogic.Interfaces.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.AccountHolderFundMessage
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseAccountHolderFundMessageTest
    {
        protected readonly Mock<IFundMessageService> MockFundMessageService = new Mock<IFundMessageService>();

        public BusinessLogic.Validators.AccountHolderFundMessageValidator GetAccountHolderFundMessageValidator()
        {
            var validator = new BusinessLogic.Validators.AccountHolderFundMessageValidator(MockFundMessageService.Object);

            return validator;
        }
    }
}
