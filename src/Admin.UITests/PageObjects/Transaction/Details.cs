using OpenQA.Selenium;
using System;
using System.Diagnostics.CodeAnalysis;

namespace Admin.UITests.PageObjects.Transaction
{
    [ExcludeFromCodeCoverage]
    class Details
    {
        private readonly IWebDriver _driver;

        public Details(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public int PaymentCount()
        {
            return PaymentsTableElement.FindElements(By.TagName("tr")).Count - 1;
        }

        public IWebElement MethodOfPaymentElement => _driver.FindElement(By.CssSelector(".ui-mop"));

        public IWebElement TotalPaymentElement => _driver.FindElement(By.CssSelector(".ui-total-amount"));

        public IWebElement VatElement => _driver.FindElement(By.CssSelector(".ui-vat-amount"));

        public IWebElement PaymentsTableElement => _driver.FindElement(By.CssSelector(".ui-payments-table"));
    }
}
