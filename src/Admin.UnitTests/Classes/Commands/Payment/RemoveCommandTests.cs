using Admin.UnitTests.Helpers;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using Command = Admin.Classes.Commands.Payment.RemoveCommand;
using CommandResult = Admin.Classes.Commands.CommandResult;
using ViewModel = Admin.Models.Payment.IndexViewModel;

namespace Admin.UnitTests.Classes.Commands.Payment
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class RemoveCommandTests
    {
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();

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

        private void SetupHttpContext(ViewModel viewModel)
        {
            HttpContext.Current = MoqHelper.FakeHttpContext();
            HttpContext.Current.Session["PaymentModel"] = viewModel;

        }

        [TestMethod]
        public void OnExecuteReturnsCommandResult()
        {
            // Arrange
            var model = GenerateViewModel();

            SetupHttpContext(model);

            var command = new Command(_mockLogger.Object);

            // Act
            var result = command.Execute("Test1");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.AreEqual(result.Success, true);
        }

        [TestMethod]
        public void NoMatchingBasketItemReturnFalseResult()
        {
            // Arrange
            var model = GenerateViewModel();

            SetupHttpContext(model);

            var command = new Command(_mockLogger.Object);

            // Act
            var result = command.Execute(string.Empty);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.AreEqual(result.Success, false);
        }

        [TestMethod]
        public void EmptyBasketReturnFalseResult()
        {
            // Arrange
            var model = GenerateViewModel();
            model.Basket = null;

            SetupHttpContext(model);

            var command = new Command(_mockLogger.Object);

            // Act
            var result = command.Execute("Test1");

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(CommandResult));
            Assert.AreEqual(result.Success, false);
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
