﻿using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Controller = Admin.Controllers.UserTemplateController;
using Dependencies = Admin.Controllers.UserTemplateControllerDependencies;

namespace Admin.UnitTests.Controllers.UserTemplate.Edit
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Post
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.Shared.BasicListViewModel, int>> _mockBasicListViewModelBuilder = new Mock<IModelBuilder<Models.Shared.BasicListViewModel, int>>();
        private readonly Mock<IModelBuilder<Models.UserTemplate.EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.UserTemplate.EditViewModel, int>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .Where(x => x.Name == "Edit")
                .FirstOrDefault();
        }

        private ActionResult GetResult(Models.UserTemplate.EditViewModel model, bool isModelValid)
        {
            var editCommand = new Mock<IModelCommand<Models.UserTemplate.EditViewModel>>();
            editCommand.Setup(x => x.Execute(It.IsAny<Models.UserTemplate.EditViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(true));

            var dependencies = new Dependencies(
                _mockLogger.Object,
                _mockBasicListViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                editCommand.Object);

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
            var result = GetResult(new Models.UserTemplate.EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
        }

        [TestMethod]
        public void ReturnsCorrectViewModelTypeIfModelInvalid()
        {
            var result = GetResult(new Models.UserTemplate.EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.UserTemplate.EditViewModel));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var result = GetResult(new Models.UserTemplate.EditViewModel(), true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToBackIfModelValid()
        {
            var result = GetResult(new Models.UserTemplate.EditViewModel(), true) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "Back");
        }

        [TestMethod]
        public void RedirectsToBackIfModelValid()
        {
            var result = GetResult(new Models.UserTemplate.EditViewModel(), true) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
