using OpenQA.Selenium;
using System;

namespace Admin.UITests.Helpers
{
    public class CustomExpectedConditions
    {
        public static Func<IWebDriver, IWebElement> ElementDoesNotHaveCssClass(IWebElement element, string cssClass)
        {
            return (driver) =>
            {
                return element.GetAttribute("class").Contains(cssClass)
                    ? null
                    : element;
            };
        }
    }
}
