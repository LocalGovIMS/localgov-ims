using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace PaymentPortal.UITests.PageObjects
{
    [ExcludeFromCodeCoverage]
    class BasketPage
    {
        private readonly IWebDriver _driver;
        private readonly string _baseUrl;

        public BasketPage(IWebDriver driver, string baseUrl)
        {
            _driver = driver;
            _baseUrl = baseUrl;
        }

        public void Load()
        {
            _driver.Navigate().GoToUrl(_baseUrl);
        }

        public void AddItemToBasket(string type, string reference, string amount)
        {
            new SelectElement(PaymentTypeDropdown).SelectByText(type);
            PaymentReferenceField.Clear();
            PaymentAmountField.Clear();
            PaymentReferenceField.SendKeys(reference);
            PaymentAmountField.SendKeys(amount);
            AddToBasketButton.Click();
        }

        public bool HasInBasket(string description)
        {
            return BasketTable.Text.Contains(description);
        }

        public int BasketItemCount()
        {
            return BasketTable.FindElements(By.TagName("tr")).Count - 1;
        }

        public void CheckoutBasket()
        {
            PayByCardButton.Click();
        }

        public IWebElement PaymentTypeDropdown => _driver.FindElement(By.Id("PaymentType"));

        public IWebElement PaymentReferenceField => _driver.FindElement(By.Id("PaymentReference"));

        public IWebElement PaymentAmountField => _driver.FindElement(By.Id("Amount"));

        public IWebElement AddToBasketButton => _driver.FindElement(By.CssSelector("button[value=Card]"));

        public IWebElement PayByCardButton => _driver.FindElement(By.CssSelector("button[value=Pay]"));

        public IList<IWebElement> FieldValidationErrors => _driver.FindElements(By.CssSelector(".field-validation-error"));

        public IWebElement BasketTable => _driver.FindElement(By.CssSelector("table"));
    }
}