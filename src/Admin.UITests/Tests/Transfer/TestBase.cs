using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Admin.UITests.Tests.Transfer
{
    [ExcludeFromCodeCoverage]
    public class TestBase
    {
        protected static IWebDriver WebDriver;
        protected static StringBuilder VerificationErrors;
        protected static string BaseUrl;
        protected static string Username;
        protected static string Password;

        [ClassInitialize(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void Initialise(TestContext context)
        {
            ChromeOptions options = new ChromeOptions();

            options.AddArgument("--start-maximized");

            WebDriver = new ChromeDriver(options);
            WebDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            BaseUrl = context.Properties["adminUrl"] as string;
            Username = context.Properties["adminUsername"] as string;
            Password = context.Properties["adminPassword"] as string;

            VerificationErrors = new StringBuilder();
        }

        [ClassCleanup(InheritanceBehavior.BeforeEachDerivedClass)]
        public static void Cleanup()
        {
            try
            {
                WebDriver.Quit();
                WebDriver.Close();
            }
            catch (Exception)
            {
                // Exceptions closing are fine
            }

            VerificationErrors.ToString().Should().BeEmpty();
        }

        [TestInitialize]
        public void Setup()
        {
            var loginPage = new PageObjects.Account.Login(WebDriver, BaseUrl);

            loginPage.LoginWithUsernameAndPassword(Username, Password);
        }
    }
}
