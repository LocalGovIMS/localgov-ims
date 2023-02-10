using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using Web.Mvc.Navigation;
using Web.UnitTests.Helpers;

namespace Web.UnitTests.Mvc.Navigation
{
    [TestClass]
    public class NavigatablePageActionFilterAttributeTests
    {
        [DataRow(false, 2)]
        [DataRow(true, 1)]
        [TestMethod]
        public void OnActionExecuted_CleanNavigationHasTheExpectedEffectOnTheStack(bool clearNavigation, int expectedNumberOfNavigationItems)
        {
            // Arrange

            // 1. Arrange - create current httpContext
            var httpContext = MoqHelper.FakeHttpContext(new HttpRequest("", "http://www.test.com/test-page2", ""));

            // 2. Arrange - actionExecutedContext
            var actionExecutedContext = new ActionExecutedContext();
            actionExecutedContext.HttpContext = new HttpContextWrapper(httpContext);

            // 3. Arrange - HttpContext
            HttpContext.Current = httpContext;

            // 4. Arange - Setup current state of the stack - load it with 1 item
            var stack = new Stack<NavigationItem>();
            stack.Push(new NavigationItem() { Url = "http://www.test.com/test-page1", Path = "/test-page1", DisplayText = "Page 1" });
            HttpContext.Current.Session["NAVIGATION__STACK"] = stack;

            // 5. Arrange - Create the navigation manager and define our attribute
            var navigationManager = new NavigationManager();
            var attribute1 = new NavigatablePageActionFilterAttribute
            {
                ClearNavigation = clearNavigation,
                DisplayText = "Page 2",
                OnlyComparePath = false,
                AddToStack = true
            };

            // Act
            attribute1.OnActionExecuted(actionExecutedContext);

            // Assert
            Assert.IsTrue(navigationManager.NavigationItems.Count == expectedNumberOfNavigationItems);
            Assert.IsTrue(navigationManager.NavigationItems.Peek().DisplayText.Equals("Page 2"));
            Assert.IsTrue(navigationManager.NavigationItems.Peek().Url.Equals("/test-page2"));
            Assert.IsTrue(navigationManager.NavigationItems.Peek().Path.Equals("/test-page2"));
        }

        [DataRow(false, 2)]
        [DataRow(true, 1)]
        [TestMethod]
        public void OnActionExecuted_OnlyComparePathHasTheExpectedEffectOnTheStack(bool onlyComparePath, int expectedNumberOfNavigationItems)
        {
            // Arrange

            // 1. Arrange - actionExecutedContext

            var actionExecutedContext = new ActionExecutedContext();
            var httpContext = MoqHelper.FakeHttpContext(new HttpRequest("", "http://www.test.com/test-page1?criteria2=test", "criteria2=test"));
            actionExecutedContext.HttpContext = new HttpContextWrapper(httpContext);

            // 2. Arrange - HttpContext

            HttpContext.Current = httpContext;

            // 3. Arange - Setup current state of the stack - load it with 1 item

            var stack = new Stack<NavigationItem>();
            stack.Push(new NavigationItem() { Url = "http://www.test.com/test-page1?criteria1=test", Path = "/test-page1", DisplayText = "Page 1" });
            HttpContext.Current.Session["NAVIGATION__STACK"] = stack;

            // 4. Arrange - Create the navigation manager and define our attribute

            var navigationManager = new NavigationManager();
            var attribute1 = new NavigatablePageActionFilterAttribute
            {
                ClearNavigation = false,
                DisplayText = "Page 2",
                OnlyComparePath = onlyComparePath,
                AddToStack = true
            };

            // Act
            attribute1.OnActionExecuted(actionExecutedContext);

            // Assert
            Assert.IsTrue(navigationManager.NavigationItems.Count == expectedNumberOfNavigationItems);
        }

        [DataRow(false, 1)]
        [DataRow(true, 2)]
        [TestMethod]
        public void OnActionExecuted_AddToStackhHasTheExpectedEffectOnTheStack(bool addToStack, int expectedNumberOfNavigationItems)
        {
            // Arrange

            // 1. Arrange - actionExecutedContext

            var actionExecutedContext = new ActionExecutedContext();
            var httpContext = MoqHelper.FakeHttpContext(new HttpRequest("", "http://www.test.com/test-page1?criteria2=test", "criteria2=test"));
            actionExecutedContext.HttpContext = new HttpContextWrapper(httpContext);

            // 2. Arrange - HttpContext

            HttpContext.Current = httpContext;

            // 3. Arange - Setup current state of the stack - load it with 1 item

            var stack = new Stack<NavigationItem>();
            stack.Push(new NavigationItem() { Url = "http://www.test.com/test-page1?criteria1=test", Path = "/test-page1", DisplayText = "Page 1" });
            HttpContext.Current.Session["NAVIGATION__STACK"] = stack;

            // 4. Arrange - Create the navigation manager and define our attribute

            var navigationManager = new NavigationManager();
            var attribute1 = new NavigatablePageActionFilterAttribute
            {
                ClearNavigation = false,
                DisplayText = "Page 2",
                OnlyComparePath = false,
                AddToStack = addToStack
            };

            // Act
            attribute1.OnActionExecuted(actionExecutedContext);

            // Assert
            Assert.IsTrue(navigationManager.NavigationItems.Count == expectedNumberOfNavigationItems);
        }

    }
}
