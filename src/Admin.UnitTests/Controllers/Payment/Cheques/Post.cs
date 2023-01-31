using Admin.Classes.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.PaymentController;

namespace Admin.UnitTests.Controllers.Payment.Cheques
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
            return GetMethod(typeof(HttpPostAttribute), nameof(Controller.Cheque));
        }

        private ActionResult GetResult(Models.Payment.Cheques model, bool isModelValid)
        {
            MockIndexViewModelBuilder.Setup(x => x.Build(It.IsAny<Models.Payment.IndexViewModel>())).Returns(new Models.Payment.IndexViewModel());
            MockEmptyBasketCommand.Setup(x => x.Execute(It.IsAny<string>())).Returns(new Admin.Classes.Commands.CommandResult(true));
            MockProcessPaymentCommand.Setup(x => x.Execute(It.IsAny<ProcessPaymentCommandAgrs>())).Returns(new Admin.Classes.Commands.CommandResult(true) { Data = "/Transaction/Details/" + "1" });

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["PaymentModel"]).Returns(new Models.Payment.IndexViewModel());

            Controller.ControllerContext = controllerContext.Object;

            if (!isModelValid)
            {
                Controller.ModelState.AddModelError("userId", "error");
            }

            return Controller.Cheque(model);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(2, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpGetAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpPostAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsCorrectEditViewIfModelInvalid()
        {
            var result = GetResult(new Models.Payment.Cheques(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == nameof(Controller.Index));
        }

        [TestMethod]
        public void ReturnsCorrectViewModelTypeIfModelInvalid()
        {
            var result = GetResult(new Models.Payment.Cheques(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.Payment.Cheques));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var result = GetResult(new Models.Payment.Cheques(), true);

            Assert.IsInstanceOfType(result, typeof(RedirectResult));
        }
    }
}
