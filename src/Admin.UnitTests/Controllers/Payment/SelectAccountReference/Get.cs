using Admin.Classes.Models;
using Admin.Controllers;
using Admin.Interfaces.Commands;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Mvc;
using Controller = Admin.Controllers.PaymentController;

namespace Admin.UnitTests.Controllers.Payment.SelectAccountReference
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
            return Controller.SelectAccountReference("123");
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
            Assert.AreEqual(result.RouteValues["action"], "Create");
        }
    }
}