using OpenQA.Selenium;
using PaymentPortal.UITests.Models;
using System.Diagnostics.CodeAnalysis;

namespace PaymentPortal.UITests.PageObjects
{
    [ExcludeFromCodeCoverage]
    class AddressPage
    {
        private readonly IWebDriver _driver;

        public AddressPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public void FillAddressDetails(Address address)
        {
            PayeeNameField.SendKeys(address.Name);
            HouseNameOrNumberField.SendKeys(address.HouseNumberOrName);
            StreetField.SendKeys(address.Street);
            CityField.SendKeys(address.Town);
            PostcodeField.SendKeys(address.PostCode);
        }

        public void SaveAddress()
        {
            NextButton.Click();
        }

        public IWebElement PayeeNameField => _driver.FindElement(By.Id("PayeeName"));

        public IWebElement HouseNameOrNumberField => _driver.FindElement(By.Id("HouseNumberOrName"));

        public IWebElement StreetField => _driver.FindElement(By.Id("Street"));

        public IWebElement CityField => _driver.FindElement(By.Id("City"));

        public IWebElement PostcodeField => _driver.FindElement(By.Id("PostCode"));

        public IWebElement NextButton => _driver.FindElement(By.Id("Next"));

        public IWebElement BackButton => _driver.FindElement(By.Id("Cancel"));
    }
}
