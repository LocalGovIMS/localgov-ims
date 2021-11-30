using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PaymentPortal.UITests.Models;
using System.Diagnostics.CodeAnalysis;

namespace PaymentPortal.UITests.PageObjects
{
    [ExcludeFromCodeCoverage]
    class SmartPayPage
    {
        private readonly IWebDriver _driver;

        public SmartPayPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void FillCardDetails(PaymentCard paymentCard)
        {
            CardNumberField.SendKeys(paymentCard.CardNumber);
            new SelectElement(ExpiryMonthDropdown).SelectByText(paymentCard.ExpiryDate.ToString("MM"));
            new SelectElement(ExpiryYearDropdown).SelectByText(paymentCard.ExpiryDate.Year.ToString());
            SecurityCodeField.SendKeys(paymentCard.SecurityCode);
            CardHolderNameField.SendKeys(paymentCard.NameOnCard);
        }

        public void FillAddressDetails(Address address)
        {
            HouseNumberOrNameField.SendKeys(address.HouseNumberOrName);
            StreetField.SendKeys(address.Street);
            CityField.SendKeys(address.Town);
            PostcodeField.SendKeys(address.PostCode);
        }

        public void SubmitPayment()
        {
            SubmitPaymentButton.Click();
        }

        public IWebElement PaymentAmountLabel => _driver.FindElement(By.Id("displayAmount"));

        public IWebElement CardNumberField => _driver.FindElement(By.Id("card.cardNumber"));

        public IWebElement CardHolderNameField => _driver.FindElement(By.Id("card.cardHolderName"));

        public IWebElement ExpiryMonthDropdown => _driver.FindElement(By.Id("card.expiryMonth"));

        public IWebElement ExpiryYearDropdown => _driver.FindElement(By.Id("card.expiryYear"));

        public IWebElement SecurityCodeField => _driver.FindElement(By.Id("card.cvcCode"));

        public IWebElement HouseNumberOrNameField => _driver.FindElement(By.Id("card.billingAddress.houseNumberOrName"));

        public IWebElement StreetField => _driver.FindElement(By.Id("card.billingAddress.street"));

        public IWebElement CityField => _driver.FindElement(By.Id("card.billingAddress.city"));

        public IWebElement PostcodeField => _driver.FindElement(By.Id("card.billingAddress.postalCode"));

        public IWebElement SubmitPaymentButton => _driver.FindElement(By.CssSelector(".paySubmitcard"));

        public IWebElement CancelPaymentButton => _driver.FindElement(By.Id("mainBack"));
    }
}
