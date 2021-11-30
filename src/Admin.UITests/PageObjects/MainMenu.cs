using OpenQA.Selenium;
using System.Diagnostics.CodeAnalysis;

namespace Admin.UITests.PageObjects
{
    [ExcludeFromCodeCoverage]
    class MainMenu
    {
        private readonly IWebDriver _driver;

        public MainMenu(IWebDriver driver, string baseUrl)
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
