using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using Web.Mvc.Navigation;
using Web.UnitTests.Helpers;

namespace Web.UnitTests.Mvc
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class NavigationManagerTests
    {
        private const string sessionKey = "NAVIGATION__STACK";

        private void SetupHttpContext()
        {
            HttpContext.Current = MoqHelper.FakeHttpContext();
            HttpContext.Current.Session[sessionKey] = new Stack<NavigationItem>();
        }

        [TestMethod]
        public void CanInstantiateWithSessionData()
        {
            // Arrange
            SetupHttpContext();

            // Act
            var navigationManager = new NavigationManager();

            // Assert
            Assert.IsNotNull(navigationManager);
        }

        [TestMethod]
        public void CanInstantiateWithoutSessionData()
        {
            // Arrange
            SetupHttpContext();
            HttpContext.Current.Session[sessionKey] = null;

            // Act
            var navigationManager = new NavigationManager();

            // Assert
            Assert.IsNotNull(navigationManager);
        }

        [TestMethod]
        public void CanPushOntoStack()
        {
            // Arrange
            SetupHttpContext();

            var navigationManager = new NavigationManager();
            var navigationItem = new NavigationItem();

            // Act
            navigationManager.Push(navigationItem, false);

            // Assert
            Assert.IsNotNull(navigationManager);
            Assert.AreEqual(navigationManager.NavigationItems.Count, 1);
        }

        [TestMethod]
        public void CannotPushIdenticalNavigationItemsConsecutively()
        {
            // Arrange
            SetupHttpContext();

            var navigationManager = new NavigationManager();
            var navigationItem = new NavigationItem();

            // Act
            navigationManager.Push(navigationItem, false);
            navigationManager.Push(navigationItem, false);

            // Assert
            Assert.IsNotNull(navigationManager);
            Assert.AreEqual(navigationManager.NavigationItems.Count, 1);
        }

        [TestMethod]
        public void IfPushingAnExistingItemRemoveAnyExistingSubsequentItems()
        {
            // Arrange
            SetupHttpContext();

            var navigationManager = new NavigationManager();
            var navigationItem1 = new NavigationItem() { Url = "Test1", Path = "Test" };
            var navigationItem2 = new NavigationItem() { Url = "Test2", Path = "Test" };

            var navigationItem = new NavigationItem();

            // Act
            navigationManager.Push(navigationItem1, false);
            navigationManager.Push(navigationItem2, false);
            navigationManager.Push(navigationItem1, false);

            // Assert
            Assert.IsNotNull(navigationManager);
            Assert.AreEqual(navigationManager.NavigationItems.Count, 1);
        }

        [TestMethod]
        public void CanReplaceAnItemIfJustThePathMatches()
        {
            // Arrange
            SetupHttpContext();

            var navigationManager = new NavigationManager();
            var navigationItem1 = new NavigationItem() { Url = "Test1", Path = "Test" };
            var navigationItem2 = new NavigationItem() { Url = "Test2", Path = "Test" };

            // Act
            navigationManager.Push(navigationItem1, false);
            navigationManager.Push(navigationItem2, true);

            // Assert
            Assert.IsNotNull(navigationManager);
            Assert.AreEqual(navigationManager.NavigationItems.Count, 1);
            Assert.AreEqual(navigationManager.NavigationItems.Peek().Url, "Test2");
        }

        [TestMethod]
        public void PopAnEmptyStackReturnsNull()
        {
            // Arrange
            SetupHttpContext();

            var navigationManager = new NavigationManager();

            // Act
            var result = navigationManager.Pop();

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public void PopReturnsLastItemAdded()
        {
            // Arrange
            SetupHttpContext();

            var navigationManager = new NavigationManager();
            var navigationItem1 = new NavigationItem() { Url = "Test1", Path = "Test" };
            var navigationItem2 = new NavigationItem() { Url = "Test2", Path = "Test" };
            // Act
            navigationManager.Push(navigationItem1, false);
            navigationManager.Push(navigationItem2, false);

            var result = navigationManager.Pop();
            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(NavigationItem));
            Assert.AreEqual(result.Url, "Test2");
        }

        [TestMethod]
        public void ClearRemoveAllItems()
        {
            // Arrange
            SetupHttpContext();

            var navigationManager = new NavigationManager();

            // Act
            navigationManager.Clear();

            // Assert
            Assert.AreEqual(navigationManager.NavigationItems.Count, 0);
        }
    }
}
