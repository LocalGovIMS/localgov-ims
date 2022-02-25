using Admin.Controllers;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.PaymentIntegrationController;

namespace Admin.UnitTests.Controllers.PaymentIntegration.Create
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Post
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.PaymentIntegration.DetailsViewModel, int>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.PaymentIntegration.DetailsViewModel, int>>();
        private readonly Mock<IModelBuilder<Models.PaymentIntegration.EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.PaymentIntegration.EditViewModel, int>>();
        private readonly Mock<IModelBuilder<IList<Models.PaymentIntegration.DetailsViewModel>, int>> _mockListViewModelBuilder = new Mock<IModelBuilder<IList<Models.PaymentIntegration.DetailsViewModel>, int>>();
        private readonly Mock<IModelCommand<Models.PaymentIntegration.EditViewModel>> _mockEditCommand = new Mock<IModelCommand<Models.PaymentIntegration.EditViewModel>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .Where(x => x.Name == "Edit")
                .FirstOrDefault();
        }

        private ActionResult GetResult(Models.PaymentIntegration.EditViewModel model, bool isModelValid)
        {
            var createCommand = new Mock<IModelCommand<Models.PaymentIntegration.EditViewModel>>();
            createCommand.Setup(x => x.Execute(It.IsAny<Models.PaymentIntegration.EditViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(true));

            var dependencies = new PaymentIntegrationControllerDependencies(
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
            var result = GetResult(new Models.PaymentIntegration.EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Create");
        }

        [TestMethod]
        public void ReturnsCorrectViewModelTypeIfModelInvalid()
        {
            var result = GetResult(new Models.PaymentIntegration.EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.PaymentIntegration.EditViewModel));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var result = GetResult(new Models.PaymentIntegration.EditViewModel(), true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToBackIfModelValid()
        {
            var result = GetResult(new Models.PaymentIntegration.EditViewModel(), true) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "Back");
        }

        [TestMethod]
        public void RedirectsToBackIfModelValid()
        {
            var result = GetResult(new Models.PaymentIntegration.EditViewModel(), true) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
