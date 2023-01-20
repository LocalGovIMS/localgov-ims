using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace Admin.UnitTests.Controllers.FundMessageMetadata.Edit
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
            return GetMethod(typeof(HttpPostAttribute), "Edit");
        }

        private ActionResult GetResult(Models.FundMessageMetadata.EditViewModel model, bool isModelValid)
        {
            MockEditCommand.Setup(x => x.Execute(It.IsAny<Models.FundMessageMetadata.EditViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(true));

            MockEditViewModelBuilder.Setup(x => x.Rebuild(It.IsAny<Models.FundMessageMetadata.EditViewModel>())).Returns(model);
            MockEditViewModelBuilder.Setup(x => x.Build(It.IsAny<int>())).Returns(model);
            MockEditViewModelBuilder.Setup(x => x.Build()).Returns(model);

            if (!isModelValid)
            {
                Controller.ModelState.AddModelError("ruleId", "error");
            }

            return Controller.Edit(model);
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
        public void ReturnsCorrectEditViewIfModelInvalid()
        {
            var result = GetResult(new Models.FundMessageMetadata.EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
        }

        [TestMethod]
        public void ReturnsCorrectViewModelTypeIfModelInvalid()
        {
            var result = GetResult(new Models.FundMessageMetadata.EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.FundMessageMetadata.EditViewModel));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var result = GetResult(new Models.FundMessageMetadata.EditViewModel(), true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToBackIfModelValid()
        {
            var result = GetResult(new Models.FundMessageMetadata.EditViewModel(), true) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "Back");
        }

        [TestMethod]
        public void RedirectsToBackIfModelValid()
        {
            var result = GetResult(new Models.FundMessageMetadata.EditViewModel(), true) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
