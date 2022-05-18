using Admin.Interfaces.Resolvers;
using Admin.UnitTests.Helpers;
using BusinessLogic.Classes.Result;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using BusinessLogic.Interfaces.Services;
using BusinessLogic.Interfaces.Validators;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using Command = Admin.Classes.Commands.Payment.CreatePaymentsCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.Payment.IndexViewModel;

namespace Admin.UnitTests.Classes.Commands.Payment
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CreatePaymentsCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IPaymentService> _mockPaymentService = new Mock<IPaymentService>();
        private readonly Mock<IBasketValidator> _mockBasketValidator = new Mock<IBasketValidator>();
        private readonly Mock<IUrlResolver> _mockUrlResolver = new Mock<IUrlResolver>();
        private readonly Mock<IMethodOfPaymentService> _mockMethodOfPaymentService = new Mock<IMethodOfPaymentService>();

        private void SetupBasketValidator(Mock<IBasketValidator> validator)
        {
            validator.Setup(x => x.Validate(
                It.IsAny<BusinessLogic.Models.Payments.Basket>()
                )).Returns(
                    new Result()
                );
        }

        private void SetupPaymentsService(Mock<IPaymentService> service)
        {
            service.Setup(x => x.CreateHppPayments(
                It.IsAny<List<BusinessLogic.Classes.PaymentDetails>>()
                )).Returns(
                    new BusinessLogic.Classes.PaymentResponse()
                );
        }

        private void SetupMethodOfPaymentService(Mock<IMethodOfPaymentService> service)
        {
            service.Setup(x => x.GetAllMops(It.IsAny<bool>()))
                .Returns(new List<Mop>() {
                    new Mop()
                    {
                        MopCode = "91",
                        Metadata = new List<MopMetadata>()
                        {
                            new MopMetadata()
                            {
                                Key = MopMetadataKeys.IsACardViaStaffPayment,
                                Value = "True"
                            }
                        }
                    }
                });
        }

        private ViewModel GenerateViewModel()
        {
            return new ViewModel()
            {
                AccountReference = "Test2",
                Address = new Admin.Models.Payment.Address()
                {
                    CallRecordingMsg = string.Empty,
                    CallRecordingMsgShown = false,
                    City = string.Empty,
                    HouseNumberOrName = string.Empty,
                    Message = null,
                    PostCode = string.Empty,
                    Street = string.Empty
                },
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
            SetupBasketValidator(_mockBasketValidator);
            SetupPaymentsService(_mockPaymentService);
            SetupMethodOfPaymentService(_mockMethodOfPaymentService);

            var command = new Command(
                _mockLogger.Object,
                _mockPaymentService.Object,
                _mockBasketValidator.Object,
                _mockUrlResolver.Object,
                _mockMethodOfPaymentService.Object);

            HttpContext.Current = MoqHelper.FakeHttpContext();

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void NoAddressReturnResult()
        {
            // Arrange
            SetupBasketValidator(_mockBasketValidator);
            SetupMethodOfPaymentService(_mockMethodOfPaymentService);

            var command = new Command(
                _mockLogger.Object,
                _mockPaymentService.Object,
                _mockBasketValidator.Object,
                _mockUrlResolver.Object,
                _mockMethodOfPaymentService.Object);

            HttpContext.Current = MoqHelper.FakeHttpContext();

            var model = GenerateViewModel();
            model.Address = null;

            // Act
            var result = command.Execute(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public void InvalidBasketReturnFailResult()
        {
            // Arrange
            SetupMethodOfPaymentService(_mockMethodOfPaymentService);

            _mockBasketValidator.Setup(x => x.Validate(
                It.IsAny<BusinessLogic.Models.Payments.Basket>()
                )).Returns(
                    new Result("Error")
                );

            var command = new Command(
                _mockLogger.Object,
                _mockPaymentService.Object,
                _mockBasketValidator.Object,
                _mockUrlResolver.Object,
                _mockMethodOfPaymentService.Object);

            HttpContext.Current = MoqHelper.FakeHttpContext();

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.AreEqual(result.Success, false);
        }
    }
}
