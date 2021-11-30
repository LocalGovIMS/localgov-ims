using OpenQA.Selenium;
using System.Diagnostics.CodeAnalysis;

namespace Admin.UITests.PageObjects.Account
{
    [ExcludeFromCodeCoverage]
    class Login
    {
        private readonly IWebDriver _driver;

        public Login(IWebDriver driver, string baseUrl)
        {
            _driver = driver;
            _driver.Navigate().GoToUrl($"{baseUrl}Account/Login");
        }

        public void LoginWithUsernameAndPassword(string username, string password)
        {
            EmailInput.SendKeys(username);
            PasswordInput.SendKeys(password);

            LoginButton.Click();
        }

        private IWebElement EmailInput => _driver.FindElement(By.CssSelector("#Email"));
        private IWebElement PasswordInput => _driver.FindElement(By.CssSelector("#Password"));
        private IWebElement LoginButton => _driver.FindElement(By.CssSelector("#submit-login"));
    }
}
