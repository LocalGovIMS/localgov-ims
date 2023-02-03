using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Admin.UnitTests.Controllers.SystemMessage.Create
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

        private ActionResult GetResult(Models.SystemMessage.EditViewModel model, bool isModelValid)
        {
            MockCreateCommand.Setup(x => x.Execute(It.IsAny<Models.SystemMessage.EditViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(true));

            if (!isModelValid)
            {
                Controller.ModelState.AddModelError("userId", "error");
            }

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
            var result = GetResult(new Models.SystemMessage.EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == nameof(Controller.Create));
        }

        [TestMethod]
        public void ReturnsCorrectViewModelTypeIfModelInvalid()
        {
            var result = GetResult(new Models.SystemMessage.EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.SystemMessage.EditViewModel));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var result = GetResult(new Models.SystemMessage.EditViewModel(), true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToBackIfModelValid()
        {
            var result = GetResult(new Models.SystemMessage.EditViewModel(), true) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "Back");
        }

        [TestMethod]
        public void RedirectsToBackIfModelValid()
        {
            var result = GetResult(new Models.SystemMessage.EditViewModel(), true) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
