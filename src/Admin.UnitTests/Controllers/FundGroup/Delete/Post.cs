using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Admin.UnitTests.Controllers.FundGroup.Delete
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
            return GetMethod(typeof(HttpPostAttribute), nameof(Controller.DeleteFundGroup));
        }

        private ActionResult GetResult(int id, bool isModelValid)
        {
            MockDeleteCommand.Setup(x => x.Execute(It.IsAny<int>())).Returns(new Admin.Classes.Commands.CommandResult(true));

            if (!isModelValid)
            {
                Controller.ModelState.AddModelError("userId", "error");
            }

            return Controller.DeleteFundGroup(id);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpPostAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpPostAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelInvalid()
        {
            var result = GetResult(1, false);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToListIfModelInvalid()
        {
            var result = GetResult(1, false) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "List");
        }

        [TestMethod]
        public void ReturnsNullModelInvalid()
        {
            var result = GetResult(1, false) as ViewResult;

            Assert.IsNull(result);
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var result = GetResult(1, true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToListIfModelValid()
        {
            var result = GetResult(1, true) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "List");
        }

        [TestMethod]
        public void RedirectsToBackIfModelValid()
        {
            var result = GetResult(1, true) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
