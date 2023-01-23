using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.PaymentController;

namespace Admin.UnitTests.Controllers.Payment.Address
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
            return GetMethod(typeof(HttpPostAttribute), nameof(Controller.Address));
        }

        private ActionResult GetResult(Models.Payment.Address model, bool isModelValid)
        {
            MockIndexViewModelBuilder.Setup(x => x.Build(It.IsAny<Models.Payment.IndexViewModel>())).Returns(new Models.Payment.IndexViewModel());
            MockEmptyBasketCommand.Setup(x => x.Execute(It.IsAny<string>())).Returns(new Admin.Classes.Commands.CommandResult(true));

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["PaymentModel"]).Returns(new Models.Payment.IndexViewModel());

            Controller.ControllerContext = controllerContext.Object;

            if (!isModelValid)
            {
                Controller.ModelState.AddModelError("userId", "error");
            }

            return Controller.Address(model);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpGetAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpPostAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsCorrectEditViewIfModelInvalid()
        {
            var result = GetResult(new Models.Payment.Address(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == nameof(Controller.Index));
        }

        [TestMethod]
        public void ReturnsCorrectViewModelTypeIfModelInvalid()
        {
            var result = GetResult(new Models.Payment.Address(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.Payment.Address));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var result = GetResult(new Models.Payment.Address(), true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToBackIfModelValid()
        {
            var result = GetResult(new Models.Payment.Address(), true) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], nameof(Controller.Pay));
        }

        [TestMethod]
        public void RedirectsToBackIfModelValid()
        {
            var result = GetResult(new Models.Payment.Address(), true) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
