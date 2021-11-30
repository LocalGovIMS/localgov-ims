using Admin.Classes.ViewHelpers;
using Admin.UnitTests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Mvc;
using Web.Mvc.Navigation;

namespace Admin.UnitTests.Classes.ViewHelpers
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class BreadcrumbHelperTests
    {
        private const string sessionKey = "NAVIGATION__STACK";

        private void SetupHttpContext(bool emptyDisplayText)
        {
            HttpContext.Current = MoqHelper.FakeHttpContext();

            var stack = new Stack<NavigationItem>();
            stack.Push(new NavigationItem() { DisplayText = emptyDisplayText ? null : "Test", Url = "Test", Path = "Test" });

            HttpContext.Current.Session[sessionKey] = stack;
        }

        [TestMethod]
        public void CanBuild()
        {
            // Arrange
            SetupHttpContext(false);

            HtmlHelper helper = null;
            var navigationItems = new Stack<NavigationItem>();

            // Act
            var result = helper.Breadcrumb(navigationItems);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CanBuildWithoutDisplayText()
        {
            // Arrange
            SetupHttpContext(true);

            HtmlHelper helper = null;
            var navigationItems = new Stack<NavigationItem>();

            // Act
            var result = helper.Breadcrumb(navigationItems);

            // Assert
            Assert.IsNotNull(result);
        }

    }
}
