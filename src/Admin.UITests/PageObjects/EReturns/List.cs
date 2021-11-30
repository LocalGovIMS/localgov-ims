using OpenQA.Selenium;
using System.Diagnostics.CodeAnalysis;

namespace Admin.UITests.PageObjects.EReturns
{
    [ExcludeFromCodeCoverage]
    class List
    {
        private readonly IWebDriver _driver;
        private readonly string _path = "/EReturn/List";

        public List(IWebDriver driver)
        {
            _driver = driver;
        }
        public void Start()
        {
            _driver.Navigate().GoToUrl(_path);
        }

        public int ResultCount()
        {
            return ResultTableElement.FindElements(By.TagName("tr")).Count - 1;
        }

        public IWebElement ReferenceElement => _driver.FindElement(By.Id("Reference"));

        public IWebElement StartDateElement => _driver.FindElement(By.Id("StartDate"));

        public IWebElement EndDateElement => _driver.FindElement(By.Id("EndDate"));

        public IWebElement AmountElement => _driver.FindElement(By.Id("Amount"));

        public IWebElement SearchButton => _driver.FindElement(By.Id("SearchButton"));

        public IWebElement ResultTableElement => _driver.FindElement(By.Id("ResultTable"));

        public IWebElement CreateButton => _driver.FindElement(By.Id("CreateButton"));

        public IWebElement ResetButton => _driver.FindElement(By.Id("clear"));

        public IWebElement ApproveButton => _driver.FindElement(By.Id("approve"));
    }
}
