using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.Payment.CheckAddressCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.Payment.IndexViewModel;

namespace Admin.UnitTests.Classes.Commands.Payment
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CheckAddressCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();

        private void SetupFundService(Mock<IFundService> service, string vatCode, bool aquireAddress)
        {
            service.Setup(x => x.GetByFundCode(
                It.IsAny<string>())).Returns(new BusinessLogic.Entities.Fund()
                {
                    FundName = "Test",
                    VatCode = vatCode,
                    VatOverride = false,
                    AquireAddress = aquireAddress
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
                    AddressLine1 = string.Empty,
                    AddressLine2 = string.Empty,
                    AddressLine3 = string.Empty,
                    AddressLine4 = string.Empty,
                    PostCode = string.Empty,
                    Message = null
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
            SetupFundService(_mockFundService, "VC1", false);

            var command = new Command(
                _mockLogger.Object,
                _mockFundService.Object);

            // Act
            var result = command.Execute(GenerateViewModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void NullAddressCausesFundAddressRequirementCheck()
        {
            // Arrange
            SetupFundService(_mockFundService, "VC1", false);

            var command = new Command(
                _mockLogger.Object,
                _mockFundService.Object);

            var model = GenerateViewModel();
            model.Address = null;

            // Act
            var result = command.Execute(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void ResultIsTrueIfAddressIsNullAndAquireAddressIsTrue()
        {
            // Arrange
            SetupFundService(_mockFundService, "VC1", true);

            var command = new Command(
                _mockLogger.Object,
                _mockFundService.Object);

            var model = GenerateViewModel();
            model.Address = null;

            // Act
            var result = command.Execute(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.AreEqual((bool)result.Data, true);
        }
    }
}
