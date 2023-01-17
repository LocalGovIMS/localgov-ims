using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Web.Mvc;
using Controller = Admin.Controllers.PaymentController;

namespace Admin.UnitTests.Controllers.Payment.Fail
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get : TestBase
    {
        public Get()
        {
            SetupController();
        }

        private ActionResult GetResult(bool vaildSession)
        {
            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["PaymentModel"]).Returns(vaildSession ? new Models.Payment.IndexViewModel() : null);

            Controller.ControllerContext = controllerContext.Object;

            return Controller.Fail();
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResult()
        {
            var result = GetResult(true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToCreate()
        {
            var result = GetResult(true) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "Create");
        }

    }
}