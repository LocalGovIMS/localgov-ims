using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using PaymentPortal.UITests.Factories;
using PaymentPortal.UITests.Models;
using PaymentPortal.UITests.PageObjects;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;

namespace PaymentPortal.UITests
{
    [TestClass]
    [TestCategory("UI")]
    [ExcludeFromCodeCoverage]
    public class PaymentBasketTests
    {
        private static IWebDriver _driver;
        private static StringBuilder _verificationErrors;
        private static string _baseUrl;

        [ClassInitialize]
        public static void SetupTest(TestContext context)
        {
            _driver = new ChromeDriver();
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            _baseUrl = context.Properties["webUrl"] as string;
            _verificationErrors = new StringBuilder();
        }

        [ClassCleanup]
        public static void CleanupTest()
        {
            try
            {
                _driver.Quit();
                _driver.Close();
            }
            catch (Exception)
            {
                // Exceptions closing are fine
            }
            _verificationErrors.ToString().Should().BeEmpty();
        }

        [TestMethod]
        public void CanAddMultipleItemsToCart()
        {
            var page = new BasketPage(_driver, _baseUrl);
            page.Load();
            page.AddItemToBasket("Fixed Penalty Notice starting FP", "FP20000050", "5.50");
            page.AddItemToBasket("Fixed Penalty Notice starting FP", "FP20000061", "5.50");

            page.HasInBasket("FP20000050").Should().BeTrue();
            page.HasInBasket("FP20000061").Should().BeTrue();
            page.BasketItemCount().Should().Be(2);
        }

        [TestMethod]
        public void CantAddDuplicateItemsToCart()
        {
            var page = new BasketPage(_driver, _baseUrl);
            page.Load();
            page.AddItemToBasket("Fixed Penalty Notice starting FP", "FP20000050", "5.50");
            page.AddItemToBasket("Fixed Penalty Notice starting FP", "FP20000050", "5.50");

            page.HasInBasket("FP20000050").Should().BeTrue();
            page.BasketItemCount().Should().Be(1);
        }

        [TestMethod]
        public void BarclaysShouldShowCorrectAmountOnCheckout()
        {
            var page = new BasketPage(_driver, _baseUrl);
            page.Load();
            page.AddItemToBasket("Fixed Penalty Notice starting FP", "FP20000050", "5.50");
            page.AddItemToBasket("Fixed Penalty Notice starting FP", "FP20000061", "5.50");
            page.CheckoutBasket();

            var addressPage = new AddressPage(_driver);
            addressPage.FillAddressDetails(new Address());
            addressPage.SaveAddress();

            var smartPayPage = new SmartPayPage(_driver);
            var returnedPaymentAmount = smartPayPage.PaymentAmountLabel.Text;

            returnedPaymentAmount.Should().Contain("11.00");
        }

        [TestMethod]
        public void PaymentWithDebitCardShouldWork()
        {
            var basketPage = new BasketPage(_driver, _baseUrl);
            basketPage.Load();
            basketPage.AddItemToBasket("Fixed Penalty Notice starting FP", "FP20000050", "5.50");
            basketPage.CheckoutBasket();

            var addressPage = new AddressPage(_driver);
            addressPage.FillAddressDetails(new Address());
            addressPage.SaveAddress();

            var smartPayPage = new SmartPayPage(_driver);
            smartPayPage.FillCardDetails(PaymentCardFactory.GetVisaDebit());
            // smartPayPage.FillAddressDetails(new Address());
            smartPayPage.SubmitPayment();

            var basketSuccess = new BasketSuccessPage(_driver);
            basketSuccess.BodyElement.Text.Contains("Successful payment").Should().BeTrue();
        }

        [TestMethod]
        public void PaymentWithCreditCardShouldWork()
        {
            var basketPage = new BasketPage(_driver, _baseUrl);
            basketPage.Load();
            basketPage.AddItemToBasket("Fixed Penalty Notice starting FP", "FP20000050", "12.50");
            basketPage.CheckoutBasket();

            var addressPage = new AddressPage(_driver);
            addressPage.FillAddressDetails(new Address());
            addressPage.SaveAddress();

            var smartPayPage = new SmartPayPage(_driver);
            smartPayPage.FillCardDetails(PaymentCardFactory.GetPaymentCard());
            // smartPayPage.FillAddressDetails(new Address());
            smartPayPage.SubmitPayment();

            var basketSuccess = new BasketSuccessPage(_driver);
            basketSuccess.BodyElement.Text.Contains("Successful payment").Should().BeTrue();
        }

        [TestMethod]
        public void PaymentWithInvalidCardShouldFail()
        {
            var basketPage = new BasketPage(_driver, _baseUrl);
            basketPage.Load();
            basketPage.AddItemToBasket("Fixed Penalty Notice starting FP", "FP20000050", "12.50");
            basketPage.CheckoutBasket();

            var addressPage = new AddressPage(_driver);
            addressPage.FillAddressDetails(new Address());
            addressPage.SaveAddress();

            var smartPayPage = new SmartPayPage(_driver);
            smartPayPage.FillCardDetails(PaymentCardFactory.GetInvalidCard());
            smartPayPage.SubmitPayment();


            var basketSuccess = new BasketSuccessPage(_driver);
            basketSuccess.BodyElement.Text.Contains("Successful payment").Should().BeFalse();
            basketSuccess.BodyElement.Text.Contains("Your payment failed").Should().BeTrue();
        }

        [TestMethod]
        public void BasketShouldBeMax10Items()
        {
            var basketPage = new BasketPage(_driver, _baseUrl);
            basketPage.Load();
            basketPage.AddItemToBasket("Fixed Penalty Notice starting FP", "FP20000050", "1");
            basketPage.AddItemToBasket("Fixed Penalty Notice starting FP", "FP20000061", "1");
            basketPage.AddItemToBasket("Fixed Penalty Notice starting FP", "FP20000083", "1");
            basketPage.AddItemToBasket("Fixed Penalty Notice starting FP", "FP20000108", "1");
            basketPage.AddItemToBasket("Fixed Penalty Notice starting FP", "FP20000119", "1");
            basketPage.AddItemToBasket("Fixed Penalty Notice starting FP", "FP2000012A", "1");
            basketPage.AddItemToBasket("Fixed Penalty Notice starting FP", "FP20000607", "1");
            basketPage.AddItemToBasket("Fixed Penalty Notice starting FP", "FP2000101A", "1");
            basketPage.AddItemToBasket("Parking Fines starting BJ", "BJ99518409", "1");
            basketPage.AddItemToBasket("Parking Fines starting BJ", "BJ99514805", "1");
            basketPage.AddItemToBasket("Parking Fines starting BJ", "BJ9951434A", "1");

            basketPage.BasketItemCount().Should().Be(10);
        }


        [TestMethod]
        public void ShouldRejectInvalidParkingFineReference()
        {
            var basketPage = new BasketPage(_driver, _baseUrl);
            basketPage.Load();
            basketPage.AddItemToBasket("Fixed Penalty Notice starting FP", "AB12345678", "100");

            basketPage.FieldValidationErrors.Any(x => x.Text == "The account reference is not valid").Should().BeTrue();
        }

        [TestMethod]
        public void ShouldAcceptValidParkingFineReference()
        {
            var basketPage = new BasketPage(_driver, _baseUrl);
            basketPage.Load();
            basketPage.AddItemToBasket("Fixed Penalty Notice starting FP", "FP20352091", "100");

            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(1);
            basketPage.FieldValidationErrors.Count.Should().Be(0);
        }

        [TestMethod]
        public void ShouldShowMessageWhenStopSet()
        {
            var basketPage = new BasketPage(_driver, _baseUrl);
            basketPage.Load();
            basketPage.AddItemToBasket("Housing Rents", "61100513680", "100");

            basketPage.FieldValidationErrors.Count.Should().Be(1);
            basketPage.FieldValidationErrors.Any(x => x.Text == "This account has been stopped").Should().BeTrue();
        }

        [TestMethod]
        public void ShouldShowErrorWhenNoFieldsEntered()
        {
            var basketPage = new BasketPage(_driver, _baseUrl);
            basketPage.Load();
            basketPage.AddItemToBasket("", "", "");

            basketPage.FieldValidationErrors.Count.Should().Be(3);
            basketPage.FieldValidationErrors.Any(x => x.Text == "A payment type must be selected").Should().BeTrue();
            basketPage.FieldValidationErrors.Any(x => x.Text == "A payment reference must be entered").Should().BeTrue();
            basketPage.FieldValidationErrors.Any(x => x.Text == "A payment amount is required").Should().BeTrue();
        }

        //  TODO: Test valid and invalid references

        //  TODO: Test only shows success message when added to basket

        //  TODO: Test basket total matches what expected

        //  TODO: Test remove from basket works

        //  TODO: Test remove all from basket works

        //  TODO: Test validation errors dont remove prefilled information

        //  TODO: Test prevent double check out for completed transaction
    }
}