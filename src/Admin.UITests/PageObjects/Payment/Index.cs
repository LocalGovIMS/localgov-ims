using OpenQA.Selenium;
using System.Diagnostics.CodeAnalysis;

namespace Admin.UITests.PageObjects.Payment
{
    [ExcludeFromCodeCoverage]
    class Index
    {
        private readonly IWebDriver _driver;

        public Index(IWebDriver driver, string baseUrl)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl(baseUrl);
        }

        public void ClickTransfersLink()
        {
            TransferMenuLink.Click();
        }

        public IWebElement TransferMenuLink => _driver.FindElement(By.CssSelector("#menu__transfer"));
    }
}
