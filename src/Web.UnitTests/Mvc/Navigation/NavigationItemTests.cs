using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Web.Mvc.Navigation;

namespace Web.UnitTests.Mvc
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class NavigationItemTests
    {
        [TestMethod]
        public void CanGetAndSetProperties()
        {
            // Arrange
            var navigationItem = new NavigationItem
            {
                DisplayText = "DisplayText",
                Url = "Url",
                Path = "Path"
            };

            // Assert
            Assert.IsNotNull(navigationItem.DisplayText);
            Assert.IsNotNull(navigationItem.Url);
            Assert.IsNotNull(navigationItem.Path);

            Assert.AreEqual(navigationItem.DisplayText, "DisplayText");
            Assert.AreEqual(navigationItem.Url, "Url");
            Assert.AreEqual(navigationItem.Path, "Path");
        }

        [TestMethod]
        public void NavigationItemIsNotEqualToNull()
        {
            // Arrange
            var navigationItem = new NavigationItem
            {
                DisplayText = "DisplayText",
                Url = "Url",
                Path = "Path"
            };

            var result = navigationItem.Equals(null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NavigationItemIsNotEqualToNullObject()
        {
            // Arrange
            var navigationItem = new NavigationItem
            {
                DisplayText = "DisplayText",
                Url = "Url",
                Path = "Path"
            };

            var result = navigationItem.Equals((object)null);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NavigationItemIsEqualToItself()
        {
            // Arrange
            var navigationItem = new NavigationItem
            {
                DisplayText = "DisplayText",
                Url = "Url",
                Path = "Path"
            };

            var result = navigationItem.Equals((object)navigationItem);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void NavigationItemIsNotEqualToAnotherObject()
        {
            // Arrange
            var navigationItem = new NavigationItem
            {
                DisplayText = "DisplayText",
                Url = "Url",
                Path = "Path"
            };

            var result = navigationItem.Equals(new StringBuilder()); // Just another type...could be anything

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void NavigationItemIsNotEqualToADifferentNavigationItem()
        {
            // Arrange
            var navigationItem1 = new NavigationItem
            {
                DisplayText = "DisplayText1",
                Url = "Url1",
                Path = "Path1"
            };

            var navigationItem2 = new NavigationItem
            {
                DisplayText = "DisplayText2",
                Url = "Url2",
                Path = "Path2"
            };

            var result = navigationItem1.Equals((object)navigationItem2);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void CanGetHashCode()
        {
            // Arrange
            var navigationItem = new NavigationItem
            {
                DisplayText = "DisplayText1",
                Url = "Url1",
                Path = "Path1"
            };

            var result = navigationItem.GetHashCode();

            // Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }

        [TestMethod]
        public void CanGetHashCodeWithoutUrl()
        {
            // Arrange
            var navigationItem = new NavigationItem
            {
                DisplayText = "DisplayText1",
                Path = "Path1"
            };

            var result = navigationItem.GetHashCode();

            // Assert
            Assert.IsInstanceOfType(result, typeof(int));
        }
    }
}
