//using BusinessLogic.Interfaces.Security;
//using BusinessLogic.Interfaces.Services;
//using log4net;
//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System.Diagnostics.CodeAnalysis;

//namespace BusinessLogic.UnitTests.Validators.AccountReferenceValidator
//{
//    [TestClass]
//    [ExcludeFromCodeCoverage]
//    public class BaseAccountReferenceValidatorTest
//    {
//        protected readonly Mock<ILog> MockLogger = new Mock<ILog>();
//        protected readonly Mock<IFundService> MockFundService = new Mock<IFundService>();
//        protected readonly Mock<IAccountHolderService> MockAccountHolderService = new Mock<IAccountHolderService>();
//        protected readonly Mock<ISecurityContext> MockSecurityContext = new Mock<ISecurityContext>();

//        public BusinessLogic.Validators.AccountReferenceValidator GetAccountReferenceValidator()
//        {
//            var validator = new BusinessLogic.Validators.AccountReferenceValidator(
//                MockLogger.Object,
//                MockFundService.Object,
//                MockAccountHolderService.Object,
//                MockSecurityContext.Object);

//            return validator;
//        }
//    }
//}
