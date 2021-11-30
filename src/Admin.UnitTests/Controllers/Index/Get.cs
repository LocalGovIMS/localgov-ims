using Admin.Controllers;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web;
using System.Web.Mvc;

namespace Admin.UnitTests.Controllers.Index
{
    [TestClass]
    public class IndexTest
    {
        [TestMethod]
        public void IndexRoutesToCreate()
        {
            var mockPaymentDependencies = new Mock<IPaymentControllerDependencies>();
            Mock<HttpSessionStateBase> mockSession = new Mock<HttpSessionStateBase>();

            var controller = new PaymentController(mockPaymentDependencies.Object);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["PaymentModelSessionKey"]).Returns("Hello World");

            controller.ControllerContext = controllerContext.Object;

            var result = controller.Index();
            result.Should().BeOfType(typeof(RedirectToRouteResult));
        }
    }
}
