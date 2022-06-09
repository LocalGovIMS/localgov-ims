using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.FundMessageController;
using Dependencies = Admin.Controllers.FundMessageControllerDependencies;

namespace Admin.UnitTests.Controllers.FundMessage.Create
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Post
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.FundMessage.DetailsViewModel, int>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.FundMessage.DetailsViewModel, int>>();
        private readonly Mock<IModelBuilder<Models.FundMessage.EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.FundMessage.EditViewModel, int>>();
        private readonly Mock<IModelBuilder<Models.FundMessage.ListViewModel, Models.FundMessage.SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<Models.FundMessage.ListViewModel, Models.FundMessage.SearchCriteria>>();
        private readonly Mock<IModelCommand<Models.FundMessage.EditViewModel>> _mockEditCommand = new Mock<IModelCommand<Models.FundMessage.EditViewModel>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .Where(x => x.Name == "Edit")
                .FirstOrDefault();
        }

        private ActionResult GetResult(Models.FundMessage.EditViewModel model, bool isModelValid)
        {
            var createCommand = new Mock<IModelCommand<Models.FundMessage.EditViewModel>>();
            createCommand.Setup(x => x.Execute(It.IsAny<Models.FundMessage.EditViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(true));

            var dependencies = new Dependencies(
                _mockLogger.Object,
                _mockDetailsViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                _mockListViewModelBuilder.Object,
                createCommand.Object,
                _mockEditCommand.Object);

            var controller = new Controller(dependencies);

            if (!isModelValid)
            {
                controller.ModelState.AddModelError("userId", "error");
            }

            return controller.Create(model);
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
            var result = GetResult(new Models.FundMessage.EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Create");
        }

        [TestMethod]
        public void ReturnsCorrectViewModelTypeIfModelInvalid()
        {
            var result = GetResult(new Models.FundMessage.EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.FundMessage.EditViewModel));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var result = GetResult(new Models.FundMessage.EditViewModel(), true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToBackIfModelValid()
        {
            var result = GetResult(new Models.FundMessage.EditViewModel(), true) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "Back");
        }

        [TestMethod]
        public void RedirectsToBackIfModelValid()
        {
            var result = GetResult(new Models.FundMessage.EditViewModel(), true) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
