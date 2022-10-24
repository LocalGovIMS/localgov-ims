using BusinessLogic.Interfaces.Services;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Command = Admin.Classes.Commands.Payment.SetAddressCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.Payment.IndexViewModel;

namespace Admin.UnitTests.Classes.Commands.Payment
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class SetAddressCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IFundService> _mockFundService = new Mock<IFundService>();
        private readonly Mock<IAccountHolderService> _mockAccountHolderService = new Mock<IAccountHolderService>();

        private void SetupFundService(Mock<IFundService> service, string vatCode, bool accountExist)
        {
            service.Setup(x => x.GetByFundCode(
                It.IsAny<string>())).Returns(new BusinessLogic.Entities.Fund()
                {
                    FundName = "Test",
                    VatCode = vatCode,
                    VatOverride = false,
                    AccountExist = accountExist
                });
        }

        private void SetupAccountHolderService(Mock<IAccountHolderService> service)
        {
            service.Setup(x => x.GetByAccountReference(It.IsAny<string>())).Returns(
                new BusinessLogic.Entities.AccountHolder()
                {
                    AddressLine1 = "Test"
                });
        }

        private ViewModel GenerateModel()
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
            SetupFundService(_mockFundService, "VC1", true);

            var command = new Command(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockAccountHolderService.Object);

            // Act
            var result = command.Execute(GenerateModel());

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void NullAddressReturnsCommandResult()
        {
            // Arrange
            SetupFundService(_mockFundService, "VC1", true);

            var command = new Command(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockAccountHolderService.Object);

            var model = GenerateModel();
            model.Address = null;

            // Act
            var result = command.Execute(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void NullAddressAndNoFundReturnsCommandResult()
        {
            // Arrange
            var command = new Command(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockAccountHolderService.Object);

            var model = GenerateModel();
            model.Address = null;

            // Act
            var result = command.Execute(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void NullAddressAccountDoesntExistReturnsCommandResult()
        {
            // Arrange
            SetupFundService(_mockFundService, "VC1", false);

            var command = new Command(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockAccountHolderService.Object);

            var model = GenerateModel();
            model.Address = null;

            // Act
            var result = command.Execute(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void IfAccountMustExistGetAccountHolderAddress()
        {
            // Arrange
            SetupFundService(_mockFundService, "VC1", true);
            SetupAccountHolderService(_mockAccountHolderService);

            var command = new Command(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockAccountHolderService.Object);

            var model = GenerateModel();
            model.Address = null;

            // Act
            var result = command.Execute(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        [TestMethod]
        public void IfAccountMustExistCleanAccountHolderAddress()
        {
            // Arrange
            SetupFundService(_mockFundService, "VC1", true);
            SetupAccountHolderService(_mockAccountHolderService);

            var command = new Command(
                _mockLogger.Object,
                _mockFundService.Object,
                _mockAccountHolderService.Object);

            var model = GenerateModel();
            model.Address = null;

            // Act
            var result = command.Execute(model);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
        }

        //[TestMethod]
        //public void ReturnedModelIsViewModel()
        //{
        //    // Arrange
        //    var command = new Command(_mockLogger.Object);

        //    // Act
        //    var result = command.Execute(string.Empty);

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(CommandResult));
        //    Assert.IsInstanceOfType(result.Data, typeof(ViewModel));
        //}

        //[TestMethod]
        //public void ReturnedModelAddressIsNull()
        //{
        //    // Arrange
        //    var command = new Command(_mockLogger.Object);

        //    // Act
        //    var result = command.Execute(string.Empty);

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(CommandResult));
        //    Assert.IsInstanceOfType(result.Data, typeof(ViewModel));
        //    Assert.IsNull(((ViewModel)result.Data).Address);
        //}

        //[TestMethod]
        //public void ReturnedModelBasketHasNoItems()
        //{
        //    // Arrange
        //    var command = new Command(_mockLogger.Object);

        //    // Act
        //    var result = command.Execute(string.Empty);

        //    // Assert
        //    Assert.IsNotNull(result);
        //    Assert.IsInstanceOfType(result, typeof(CommandResult));
        //    Assert.IsInstanceOfType(result.Data, typeof(ViewModel));
        //    Assert.AreEqual(((ViewModel)result.Data).Basket.Count, 0);
        //}
    }
}
