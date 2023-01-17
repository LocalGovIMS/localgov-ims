using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Web.Mvc;
using Controller = Admin.Controllers.PaymentController;

namespace Admin.UnitTests.Controllers.Payment.Success
{
    [TestClass]
    public class Get : TestBase
    {
        public Get()
        {
            SetupController();
        }

        private ActionResult GetResult()
        {
            return Controller.Success("123");
        }

        [TestMethod]
        public void ReturnsARedirect()
        {
            var result = GetResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void ReturnsRedirectToAddress()
        {
            var result = GetResult() as RedirectToRouteResult;

            Assert.IsNotNull(result);
            Assert.AreEqual(result.RouteValues["action"], "Details");
        }
    }
}