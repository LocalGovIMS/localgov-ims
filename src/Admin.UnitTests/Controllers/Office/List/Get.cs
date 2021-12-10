﻿using Admin.Controllers;
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
using Web.Mvc.Navigation;
using Controller = Admin.Controllers.OfficeController;
using Dependencies = Admin.Controllers.OfficeControllerDependencies;

namespace Admin.UnitTests.Controllers.Office.List
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(OfficeController);

        private readonly Mock<IModelBuilder<Models.Office.DetailsViewModel, string>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.Office.DetailsViewModel, string>>();
        private readonly Mock<IModelBuilder<Models.Office.EditViewModel, string>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.Office.EditViewModel, string>>();
        private readonly Mock<IModelCommand<Models.Office.EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<Models.Office.EditViewModel>>();
        private readonly Mock<IModelCommand<Models.Office.EditViewModel>> _mockEditCommand = new Mock<IModelCommand<Models.Office.EditViewModel>>();
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.Name == "List")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            var listViewModelBuilder = new Mock<IModelBuilder<IList<Models.Office.DetailsViewModel>, string>>();
            listViewModelBuilder.Setup(x => x.Build()).Returns(new List<Models.Office.DetailsViewModel>());

            var dependencies = new Dependencies(
                _mockLogger.Object
                , _mockDetailsViewModelBuilder.Object
                , _mockEditViewModelBuilder.Object
                , listViewModelBuilder.Object
                , _mockCreateCommand.Object
                , _mockEditCommand.Object);

            var controller = new Controller(dependencies);

            return controller.List();
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(2, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleNavigatablePageActionFilterAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(NavigatablePageActionFilterAttribute)).Count());
        }

        [TestMethod]
        public void NavigatablePageActionFilterAttributeHasCorrectDisplayText()
        {
            var attribute = GetMethod().CustomAttributes.Single(ca => ca.AttributeType == typeof(NavigatablePageActionFilterAttribute));

            var namedArgument = attribute.NamedArguments.Where(x => x.MemberName == "DisplayText").First();

            Assert.AreEqual("Offices", namedArgument.TypedValue.Value);
        }

        [TestMethod]
        public void HasASingleHttpGetAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpGetAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsAView()
        {
            var result = GetResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void ReturnsCorrectViewName()
        {
            var result = GetResult() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "List");
        }

        [TestMethod]
        public void ReturnsAViewModel()
        {
            var result = GetResult() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void ReturnsCorrectViewModelType()
        {
            var result = GetResult() as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(List<Models.Office.DetailsViewModel>));
        }
    }
}