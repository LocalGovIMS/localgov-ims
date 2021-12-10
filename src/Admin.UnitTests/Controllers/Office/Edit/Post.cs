﻿using Admin.Interfaces.Commands;
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
using Controller = Admin.Controllers.OfficeController;
using Dependencies = Admin.Controllers.OfficeControllerDependencies;

namespace Admin.UnitTests.Controllers.Office.Edit
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Post
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.Office.DetailsViewModel, string>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.Office.DetailsViewModel, string>>();
        private readonly Mock<IModelBuilder<Models.Office.EditViewModel, string>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.Office.EditViewModel, string>>();
        private readonly Mock<IModelBuilder<IList<Models.Office.DetailsViewModel>, string>> _mockListViewModelBuilder = new Mock<IModelBuilder<IList<Models.Office.DetailsViewModel>, string>>();
        private readonly Mock<IModelCommand<Models.Office.EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<Models.Office.EditViewModel>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .Where(x => x.Name == "Edit")
                .FirstOrDefault();
        }

        private ActionResult GetResult(Models.Office.EditViewModel model, bool isModelValid)
        {
            var editCommand = new Mock<IModelCommand<Models.Office.EditViewModel>>();
            editCommand.Setup(x => x.Execute(It.IsAny<Models.Office.EditViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(true));

            var dependencies = new Dependencies(
                _mockLogger.Object,
                _mockDetailsViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                _mockListViewModelBuilder.Object,
                _mockCreateCommand.Object,
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
            var result = GetResult(new Models.Office.EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
        }

        [TestMethod]
        public void ReturnsCorrectViewModelTypeIfModelInvalid()
        {
            var result = GetResult(new Models.Office.EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(Models.Office.EditViewModel));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var result = GetResult(new Models.Office.EditViewModel(), true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToBackIfModelValid()
        {
            var result = GetResult(new Models.Office.EditViewModel(), true) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "Back");
        }

        [TestMethod]
        public void RedirectsToBackIfModelValid()
        {
            var result = GetResult(new Models.Office.EditViewModel(), true) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}