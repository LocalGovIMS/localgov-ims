using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System.Diagnostics.CodeAnalysis;

namespace Admin.UITests.PageObjects.EReturns
{
    [ExcludeFromCodeCoverage]
    class Edit
    {
        private readonly IWebDriver _driver;
        private readonly string _path = "/EReturn/Edit";

        public Edit(IWebDriver driver)
        {
            _driver = driver;
        }
        public void Start(int id)
        {
            _driver.Navigate().GoToUrl(_path + '/' + id);
        }

        public void CreateTemplate(string template, string type)
        {
            var actions = new Actions(_driver);
            TemplateElement.Click();
            actions.SendKeys(template).SendKeys(Keys.Enter).Build().Perform();

            actions = new Actions(_driver);
            TypeElement.Click();
            actions.SendKeys(type).SendKeys(Keys.Enter).Build().Perform();

            SaveButton.Click();
        }

        public IWebElement TemplateElement => _driver.FindElement(By.CssSelector(".templates-dropdown"));

        public IWebElement TypeElement => _driver.FindElement(By.CssSelector(".types-dropdown"));

        public IWebElement SaveButton => _driver.FindElement(By.CssSelector("input[value='Save']"));
    }
}
