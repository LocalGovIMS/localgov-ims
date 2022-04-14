using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.Basket
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BaseBasket
    {
        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
        protected readonly Mock<IFundService> MockFundService = new Mock<IFundService>();
        protected readonly Mock<IAccountHolderService> MockAccountHolderService = new Mock<IAccountHolderService>();
        protected readonly Mock<IPaymentValidationHandler> MockPaymentValidationHandler = new Mock<IPaymentValidationHandler>();
        protected readonly Mock<ISecurityContext> MockSecurityContextValidator = new Mock<ISecurityContext>();


        public BusinessLogic.Validators.BasketValidator GetBasket()
        {
            var validator = new BusinessLogic.Validators.BasketValidator(
                MockLogger.Object,
                MockFundService.Object,
                MockAccountHolderService.Object,
                MockPaymentValidationHandler.Object,
                MockSecurityContextValidator.Object);

            return validator;
        }
    }
}
