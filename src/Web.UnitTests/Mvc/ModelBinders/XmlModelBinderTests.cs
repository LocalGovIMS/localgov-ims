using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Serialization;
using Web.Mvc;

namespace Web.UnitTests.Mvc.ModelBinders
{
    [TestClass]
    public class XmlModelBinderTests
    {
        [TestMethod]
        public void XmlModelBinder_deserialises_correctly()
        {
            // Arrange
            var bindingContext = new ModelBindingContext
            {
                ModelMetadata = ModelMetadataProviders.Current.GetMetadataForType(null, typeof(MyClass))
            };

            var myStream = new System.IO.MemoryStream();

            var serializer = new XmlSerializer(typeof(MyClass));
            serializer.Serialize(myStream, new MyClass() { Id = 1, Name = "Test" });

            myStream.Position = 0;

            var mockRequest = new Mock<HttpRequestBase>();
            mockRequest.SetupGet(x => x.InputStream)
                .Returns(myStream);

            var mockHttpContext = new Mock<HttpContextBase>();
            mockHttpContext.SetupGet(x => x.Request)
                .Returns(mockRequest.Object);

            var mockControllerContext = new Mock<ControllerContext>();
            mockControllerContext.SetupGet(x => x.HttpContext)
                .Returns(mockHttpContext.Object);

            var modelBinder = new XmlModelBinder();

            // Act    
            MyClass actual = (MyClass)modelBinder.BindModel(mockControllerContext.Object, bindingContext);

            myStream.Dispose();

            // Assert
            actual.Id.Should().Be(1);
            actual.Name.Should().Be("Test");
        }
    }

    public class MyClass
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
