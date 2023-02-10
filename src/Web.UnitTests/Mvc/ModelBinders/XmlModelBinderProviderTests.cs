using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using Web.Mvc;
using Web.UnitTests.Helpers;

namespace Web.UnitTests.Mvc.ModelBinders
{
    [TestClass]
    public class XmlModelBinderProviderTests
    {

        [TestMethod]
        public void Returns_XmlModelBinder_When_ContentType_Is_TextXml()
        {
            // Arrange
            HttpContext.Current = MoqHelper.FakeHttpContext("text/xml");
            var xmlModelBinderProvider = new XmlModelBinderProvider();

            // Act
            var result = xmlModelBinderProvider.GetBinder(typeof(MyTestClass));

            // Assert
            Assert.IsInstanceOfType<XmlModelBinder>(result);
        }

        [TestMethod]
        public void Returns_Null_When_ContentType_Is_Not_TextXml()
        {
            // Arrange
            HttpContext.Current = MoqHelper.FakeHttpContext("application/json");
            var xmlModelBinderProvider = new XmlModelBinderProvider();

            // Act
            var result = xmlModelBinderProvider.GetBinder(typeof(MyTestClass));

            // Assert
            Assert.IsNull(result);
        }
    }

    public class MyTestClass
    {
        public MyTestClass()
        {
        }
    }
}
