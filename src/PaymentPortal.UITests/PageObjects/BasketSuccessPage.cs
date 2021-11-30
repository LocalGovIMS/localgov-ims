using OpenQA.Selenium;
using System.Diagnostics.CodeAnalysis;

namespace PaymentPortal.UITests.PageObjects
{
    [ExcludeFromCodeCoverage]
    class BasketSuccessPage
    {
        private readonly IWebDriver _driver;

        public BasketSuccessPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public IWebElement BodyElement => _driver.FindElement(By.TagName("Body"));
    }
}
