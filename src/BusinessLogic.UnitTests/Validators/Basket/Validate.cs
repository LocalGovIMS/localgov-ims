using BusinessLogic.Classes.Result;
using BusinessLogic.Enums;
using BusinessLogic.Validators.Payment;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;

namespace BusinessLogic.UnitTests.Validators.Basket
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Validate : BaseBasket
    {
        private const string DefaultErrorMessage = "There is an error with the basket";

        [TestMethod]
        public void ValidateBasketCatchExceptionReturnsDefaultError()
        {
            var basketValidator = GetBasket();
            var result = basketValidator.Validate(null);

            result.Success.Should().BeFalse();
            result.Error.Should().Be(DefaultErrorMessage);
            result.Should().BeOfType(expectedType: typeof(Result));
        }

        [TestMethod]
        public void ValidateBasketcheckDuolicatesReturnsDefaultError()
        {
            BusinessLogic.Models.Payments.Basket basket = new BusinessLogic.Models.Payments.Basket()
            {
                Items = new System.Collections.Generic.List<BusinessLogic.Models.Payments.BasketItem>()
                {
                    new BusinessLogic.Models.Payments.BasketItem()
                    {
                        AccountReference="123",
                        FundCode="12",
                        FundName="MOCKTEST",
                        Amount=100,
                        VatCode="0",
                        Narrative="test narrative"
                    }
                }
            };

            var basketValidator = GetBasket();
            var result = basketValidator.Validate(basket);

            result.Success.Should().BeFalse();
            result.Error.Should().Be(DefaultErrorMessage);
            result.Should().BeOfType(expectedType: typeof(Result));
        }

        [TestMethod]
        public void ValidateBasketIsValid()
        {
            BusinessLogic.Models.Payments.Basket basket = new BusinessLogic.Models.Payments.Basket()
            {
                Items = new System.Collections.Generic.List<BusinessLogic.Models.Payments.BasketItem>()
                {
                    new BusinessLogic.Models.Payments.BasketItem()
                    {
                        AccountReference="123",
                        FundCode="12",
                        FundName="MOCKTEST",
                        Amount=100,
                        VatCode="0",
                        Narrative="test narrative"
                    }
                }
            };

            MockPaymentValidationHandler.Setup(x => x.Validate(It.IsAny<PaymentValidationArgs>()))
                .Returns(new Result());

            var basketValidator = GetBasket();
            var result = basketValidator.Validate(basket);
            result.Success.Should().BeTrue();
            result.Should().BeOfType(expectedType: typeof(Result));
        }


        [TestMethod]
        public void ValidateBasketWithDuplicatesIsInvalid()
        {
            BusinessLogic.Models.Payments.Basket basket = new BusinessLogic.Models.Payments.Basket()
            {
                Items = new System.Collections.Generic.List<BusinessLogic.Models.Payments.BasketItem>()
                {
                    new BusinessLogic.Models.Payments.BasketItem()
                    {
                        AccountReference="123",
                        FundCode="12",
                        FundName="MOCKTEST",
                        Amount=100,
                        VatCode="0",
                        Narrative="test narrative"
                    },
                    new BusinessLogic.Models.Payments.BasketItem()
                    {
                        AccountReference="123",
                        FundCode="12",
                        FundName="MOCKTEST",
                        Amount=100,
                        VatCode="0",
                        Narrative="test narrative"
                    }
                }
            };

            MockPaymentValidationHandler.Setup(x => x.Validate(It.IsAny<PaymentValidationArgs>()))
                .Returns(new Result());

            var basketValidator = GetBasket();
            var result = basketValidator.Validate(basket);
            result.Success.Should().BeFalse();
            result.Should().BeOfType(expectedType: typeof(Result));
        }

        [TestMethod]
        public void ValidateBasketValidateRefErrorMessageReturned()
        {
            BusinessLogic.Models.Payments.Basket basket = new BusinessLogic.Models.Payments.Basket()
            {
                Items = new System.Collections.Generic.List<BusinessLogic.Models.Payments.BasketItem>()
                {
                    new BusinessLogic.Models.Payments.BasketItem()
                    {
                        AccountReference="123",
                        FundCode="12",
                        FundName="MOCKTEST",
                        Amount=100,
                        VatCode="0",
                        Narrative="test narrative"
                    }
                }
            };

            string errorMsg = "Test error message";
            MockPaymentValidationHandler.Setup(x => x.Validate(It.IsAny<PaymentValidationArgs>()))
                .Returns(new Result(errorMsg));

            var basketValidator = GetBasket();
            var result = basketValidator.Validate(basket);
            result.Success.Should().BeFalse();
            result.Error.Should().Be(errorMsg);
            result.Should().BeOfType(expectedType: typeof(Result));
        }
    }
}
