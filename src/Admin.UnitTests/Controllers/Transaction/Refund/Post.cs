using Admin.Models.Transaction;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Admin.UnitTests.Controllers.Transaction.Refund
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Post : TestBase
    {
        public Post()
        {
            SetupController();
        }

        private MethodInfo GetMethod()
        {
            return GetMethod(typeof(HttpPostAttribute), nameof(Controller.Refund));
        }

        private ActionResult GetResult(RefundViewModel model, bool success)
        {
            MockRefundCommand.Setup(x => x.Execute(
                It.IsAny<RefundViewModel>()))
                .Returns(new Admin.Classes.Commands.CommandResult(success, "TestMessage"));

            return Controller.Refund(model);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(2, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpPostAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpPostAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsRedirectResult()
        {
            var result = GetResult(new RefundViewModel() { Reference = "Test" }, true);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectsToDetails()
        {
            var result = GetResult(new RefundViewModel() { Reference = "Test" }, true);

            Assert.IsNotNull(result);
            Assert.AreEqual(nameof(Controller.Details), ((RedirectToRouteResult)result).RouteValues["action"]);
        }

        [TestMethod]
        public void RedirectsToDetailsWithSameRefundReference()
        {
            var result = GetResult(new RefundViewModel() { Reference = "Test" }, true);

            Assert.IsNotNull(result);
            Assert.AreEqual("Test", ((RedirectToRouteResult)result).RouteValues["id"]);
        }
    }
}
