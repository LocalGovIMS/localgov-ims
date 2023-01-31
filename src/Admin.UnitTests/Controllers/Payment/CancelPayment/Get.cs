using Admin.UnitTests.Controllers.Payment;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Controller = Admin.Controllers.PaymentController;

namespace Admin.UnitTests.Controllers.CancelPayment
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
            return Controller.CancelPayment();
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResult()
        {
            var result = GetResult();

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToCreate()
        {
            var result = GetResult() as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], nameof(Controller.Create));
        }
    }
}