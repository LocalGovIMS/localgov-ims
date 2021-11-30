using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Security;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.Payment.AddCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.Payment.IndexViewModel;

namespace Admin.UnitTests.Classes.Commands.Payment
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class AddCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();
        private readonly Mock<IMethodOfPaymentService> _mockMopService = new Mock<IMethodOfPaymentService>();
        private readonly Mock<IAccountReferenceValidator> _mockAccountReferenceValidator = new Mock<IAccountReferenceValidator>();
        private readonly Mock<ISecurityContext> _mockSecurityContext = new Mock<ISecurityContext>();

        private void SetupFundService(Mock<IFundService> service, string vatCode)
        {
            service.Setup(x => x.GetByFundCode(
                It.IsAny<string>())).Returns(new BusinessLogic.Entities.Fund()
                {
                    FundName = "Test",
                    VatCode = vatCode,
                    VatOverride = false
                });
        }

        private void SetupAccountReferenceValidator(Mock<IAccountReferenceValidator> item, bool isValid)
        {
            item.Setup(x => x.ValidateReference(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<AccountReferenceValidationSource>()))
                .Returns(isValid ? new BusinessLogic.Classes.Result.Result() : new BusinessLogic.Classes.Result.Result("test"));
        }

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                AccountReference = "Test2",
                Address = null,
                AddressReviewed = false,
                Amount = 1,
                Basket = new BusinessLogic.Models.Payments.Basket()
                {
                    Items = new List<BusinessLogic.Models.Payments.BasketItem>()
                    {
                        {
                            new BusinessLogic.Models.Payments.BasketItem()
                            {
                                AccountReference = "Test1",
                                Amount = 1,
                                FundCode = "F1",
                                FundName = "Test",
                                Narrative = string.Empty,
                                VatCode = string.Empty
                            }
                        }
                    }
                },
                FundCode = "F1",
                Funds = null,
                Message = null,
                Narrative = string.Empty,
                SearchEnabledFundCodes = string.Empty,
                VatCode = "VC1",
                VatCodes = null
            };
        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            SetupFundService(_mockFundService, "VC1");
            SetupAccountReferenceValidator(_mockAccountReferenceValidator, true);

            var command = new Command(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockMopService.Object,
                _mockAccountReferenceValidator.Object,
                _mockSecurityContext.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void DuplicateAccountReferenceCausesError()
        {
            // Arrange
            SetupFundService(_mockFundService, "VC1");
            SetupAccountReferenceValidator(_mockAccountReferenceValidator, true);

            var command = new Command(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockMopService.Object,
                _mockAccountReferenceValidator.Object,
                _mockSecurityContext.Object);

            var model = GenerateViewModel();
            model.AccountReference = "Test1";

            // Act
            var result = command.Execute(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public void RequiredMatchingVatCodeIsNotMatchingCausesError()
        {
            // Arrange
            SetupFundService(_mockFundService, "VC1");
            SetupAccountReferenceValidator(_mockAccountReferenceValidator, true);

            var command = new Command(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockMopService.Object,
                _mockAccountReferenceValidator.Object,
                _mockSecurityContext.Object);

            var model = GenerateViewModel();
            model.VatCode = "VC2";

            // Act
            var result = command.Execute(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public void InvalidAccountReferenceCausesError()
        {
            // Arrange
            SetupFundService(_mockFundService, "VC1");
            SetupAccountReferenceValidator(_mockAccountReferenceValidator, false);

            var command = new Command(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockMopService.Object,
                _mockAccountReferenceValidator.Object,
                _mockSecurityContext.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.AreEqual(result.Success, false);
        }
    }
}
