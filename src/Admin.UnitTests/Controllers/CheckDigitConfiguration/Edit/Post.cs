using Admin.Controllers;
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
using Controller = Admin.Controllers.CheckDigitConfigurationController;

namespace Admin.UnitTests.Controllers.CheckDigitConfiguration.Edit
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Post
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.CheckDigitConfiguration.ListViewModel, Models.CheckDigitConfiguration.SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<Models.CheckDigitConfiguration.ListViewModel, Models.CheckDigitConfiguration.SearchCriteria>>();
        private readonly Mock<IModelBuilder<Models.CheckDigitConfiguration.DetailsViewModel, int>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.CheckDigitConfiguration.DetailsViewModel, int>>();
        private readonly Mock<IModelBuilder<Models.CheckDigitConfiguration.EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.CheckDigitConfiguration.EditViewModel, int>>();
        private readonly Mock<IModelCommand<Models.CheckDigitConfiguration.EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<Models.CheckDigitConfiguration.EditViewModel>>();
        private readonly Mock<IModelCommand<int>> _mockDeleteCommand = new Mock<IModelCommand<int>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .Where(x => x.Name == "Edit")
                .FirstOrDefault();
        }

        private ActionResult GetResult(Models.CheckDigitConfiguration.EditViewModel model, bool isModelValid)
        {
            var editCommand = new Mock<IModelCommand<Models.CheckDigitConfiguration.EditViewModel>>();
            editCommand.Setup(x => x.Execute(It.IsAny<Models.CheckDigitConfiguration.EditViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(true));

            var dependencies = new CheckDigitConfigurationControllerDependencies(
                _mockLogger.Object,
                _mockDetailsViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                _mockListViewModelBuilder.Object,
                _mockCreateCommand.Object,
                editCommand.Object,
                _mockDeleteCommand.Object);

            var controller = new Controller(dependencies);

            if (!isModelValid)
            {
                controller.ModelState.AddModelError("userId", "error");
            }

            return controller.Edit(model);
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
            var result = GetResult(new Models.CheckDigitConfiguration.EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
        }

        [TestMethod]
        public void ReturnsCorrectViewModelTypeIfModelInvalid()
        {
            var result = GetResult(new Models.CheckDigitConfiguration.EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.CheckDigitConfiguration.EditViewModel));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var result = GetResult(new Models.CheckDigitConfiguration.EditViewModel(), true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToBackIfModelValid()
        {
            var result = GetResult(new Models.CheckDigitConfiguration.EditViewModel(), true) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "Back");
        }

        [TestMethod]
        public void RedirectsToBackIfModelValid()
        {
            var result = GetResult(new Models.CheckDigitConfiguration.EditViewModel(), true) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
