using Admin.UITests.Helpers;
using BusinessLogic.Models;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace Admin.UITests.PageObjects.Transfer
{
    [ExcludeFromCodeCoverage]
    class Index
    {
        private readonly IWebDriver _driver;
        private readonly int _delay = 250;

        public Index(IWebDriver driver)
        {
            _driver = driver;
        }

        public void Start()
        {
            throw new NotImplementedException();
        }

        public void AddSourceItem(TransferItem item)
        {
            ProcessTransferItem(
                SourceFundElement,
                SourceAccountReferenceElement,
                SourceVatElement,
                SourceAmountElement,
                AddSourceButton,
                item);
        }

        public void AddTargetItem(TransferItem item)
        {
            ProcessTransferItem(
                TargetFundElement,
                TargetAccountReferenceElement,
                TargetVatElement,
                TargetAmountElement,
                AddTransferButton,
                item);
        }

        private void ProcessTransferItem(
            IWebElement fundElement,
            IWebElement accountReferenceElement,
            IWebElement vatElement,
            IWebElement amountElement,
            IWebElement addButtonElement,
            TransferItem item)
        {
            var actions = new Actions(_driver);

            Thread.Sleep(_delay);

            fundElement.Click();
            actions.SendKeys(item.FundCode).SendKeys(Keys.Enter).Build().Perform();

            Thread.Sleep(_delay);

            accountReferenceElement.Click();
            accountReferenceElement.Clear();
            accountReferenceElement.SendKeys(item.AccountReference);

            Thread.Sleep(_delay);

            var waitForVatElement = new WebDriverWait(_driver, TimeSpan.FromMinutes(1));
            var clickableVatElement = waitForVatElement.Until(CustomExpectedConditions.ElementDoesNotHaveCssClass(vatElement, "disabled"));

            Thread.Sleep(_delay);

            actions = new Actions(_driver);
            clickableVatElement.Click();
            actions.SendKeys(item.VatCode).SendKeys(Keys.Enter).Build().Perform();

            Thread.Sleep(_delay);

            amountElement.Clear();
            amountElement.SendKeys(item.Amount.ToString());

            Thread.Sleep(_delay);

            addButtonElement.Click();

            Thread.Sleep(_delay);
        }

        public bool IsTransferEnabled()
        {
            return !SubmitTransferButton.GetAttribute("class").Contains("disabled");
        }

        public void SubmitTransfer()
        {
            Thread.Sleep(1000);
            if (IsTransferEnabled())
            {
                SubmitTransferButton.Click();
            }
        }

        public IWebElement SourceFundElement => _driver.FindElement(By.CssSelector(".sourceItem"));

        public IWebElement SourceVatElement => _driver.FindElement(By.CssSelector(".sourceVat"));

        public IWebElement SourceAccountReferenceElement => _driver.FindElement(By.CssSelector("#SourceItem_AccountReference"));

        public IWebElement SourceAmountElement => _driver.FindElement(By.CssSelector("#SourceItem_Amount"));

        public IWebElement TargetFundElement => _driver.FindElement(By.CssSelector(".targetItem"));

        public IWebElement TargetVatElement => _driver.FindElement(By.CssSelector(".targetVat"));

        public IWebElement TargetAccountReferenceElement => _driver.FindElement(By.CssSelector("#TransferItem_AccountReference"));

        public IWebElement TargetAmountElement => _driver.FindElement(By.CssSelector("#TransferItem_Amount"));

        public IWebElement SubmitTransferButton => _driver.FindElement(By.CssSelector(".submit-transfer"));

        public IWebElement AddTransferButton => _driver.FindElement(By.CssSelector(".add-transfer"));

        public IWebElement AddSourceButton => _driver.FindElement(By.CssSelector(".add-source"));
    }
}
