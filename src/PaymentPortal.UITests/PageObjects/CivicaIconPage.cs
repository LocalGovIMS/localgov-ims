using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using PaymentPortal.UITests.Models;
using System.Diagnostics.CodeAnalysis;

namespace PaymentPortal.UITests.PageObjects
{
    [ExcludeFromCodeCoverage]
    class CivicaIconPage
    {
        private readonly IWebDriver _driver;

        public CivicaIconPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public string GetPaymentAmount()
        {
            var amountString = PaymentAmount.Text;
            return amountString.Substring(amountString.LastIndexOf('£') + 1);
        }

        public void FillCardDetails(PaymentCard paymentCard)
        {
            CardNumberField.SendKeys(paymentCard.CardNumber);
            new SelectElement(ExpiryDateMonthDropdown).SelectByText(paymentCard.ExpiryDate.Month.ToString());
            new SelectElement(ExpiryDateYearDropdown).SelectByText(paymentCard.ExpiryDate.Year.ToString());
            SecurityCodeField.SendKeys(paymentCard.SecurityCode);
            NameOnCardField.SendKeys(paymentCard.NameOnCard);
        }

        public void FillAddressDetails(Address address)
        {
            HouseNumberField.SendKeys(address.HouseNumberOrName);
            StreetField.SendKeys(address.Street);
            TownField.SendKeys(address.Town);
            PostcodeField.SendKeys(address.PostCode);
        }

        public void SubmitPayment()
        {
            SubmitPaymentButton.Click();
        }

        #region Web Elements

        public IWebElement PaymentAmount => _driver.FindElement(By.Id("lblPaymentAmount"));

        public IWebElement CardNumberField => _driver.FindElement(By.Id("CardCapturePage_txtCardNumber"));

        public IWebElement ExpiryDateMonthDropdown => _driver.FindElement(By.Id("CardCapturePage_cboEndMonth"));

        public IWebElement ExpiryDateYearDropdown => _driver.FindElement(By.Id("CardCapturePage_cboEndYear"));

        public IWebElement IssueNumberField => _driver.FindElement(By.Id("CardCapturePage_txtIssueNumber"));

        public IWebElement SecurityCodeField => _driver.FindElement(By.Id("CardCapturePage_txtSecurityCode"));

        public IWebElement NameOnCardField => _driver.FindElement(By.Id("CardCapturePage_txtName"));

        public IWebElement HouseNumberField => _driver.FindElement(By.Id("CardCapturePage_txtHouseNo"));

        public IWebElement StreetField => _driver.FindElement(By.Id("CardCapturePage_txtStreet"));

        public IWebElement TownField => _driver.FindElement(By.Id("CardCapturePage_txtTown"));

        public IWebElement PostcodeField => _driver.FindElement(By.Id("CardCapturePage_txtPostCode"));

        public IWebElement SubmitPaymentButton => _driver.FindElement(By.Id("CardCapturePage_btnSubmit"));

        public IWebElement CancelPaymentButton => _driver.FindElement(By.Id("CardCapturePage_btnCancel"));

        #endregion
    }
}
