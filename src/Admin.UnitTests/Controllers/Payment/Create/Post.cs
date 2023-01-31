using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.PaymentController;

namespace Admin.UnitTests.Controllers.Payment.Create
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
            return GetMethod(typeof(HttpPostAttribute), nameof(Controller.Create));
        }

        private ActionResult GetResult(Models.Payment.IndexViewModel model, bool success)
        {
            MockIndexViewModelBuilder.Setup(x => x.Build()).Returns(new Models.Payment.IndexViewModel());
            MockAddCommand.Setup(x => x.Execute(It.IsAny<Models.Payment.IndexViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(success));
            MockSetAddressCommand.Setup(x => x.Execute(It.IsAny<Models.Payment.IndexViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(success));

            return Controller.Create(model);
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
        public void ReturnsCorrectEditViewIfModelInvalid()
        {
            var result = GetResult(new Models.Payment.IndexViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == nameof(Controller.Index));
        }

        [TestMethod]
        public void ReturnsCorrectViewModelTypeIfModelInvalid()
        {
            var result = GetResult(new Models.Payment.IndexViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.Payment.IndexViewModel));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var result = GetResult(new Models.Payment.IndexViewModel(), true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToBackIfModelValid()
        {
            var result = GetResult(new Models.Payment.IndexViewModel(), true) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], nameof(Controller.Create));
        }

        [TestMethod]
        public void RedirectsToBackIfModelValid()
        {
            var result = GetResult(new Models.Payment.IndexViewModel(), true) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
