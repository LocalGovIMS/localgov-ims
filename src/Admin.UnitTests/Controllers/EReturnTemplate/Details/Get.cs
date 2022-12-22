﻿using Admin.Controllers;
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
using Web.Mvc.Navigation;

namespace Admin.UnitTests.Controllers.EReturnTemplate.Details
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(EReturnTemplateController);
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.EReturnTemplate.ListViewModel, Models.EReturnTemplate.SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<Models.EReturnTemplate.ListViewModel, Models.EReturnTemplate.SearchCriteria>>();
        private readonly Mock<IModelBuilder<Models.EReturnTemplate.DetailsViewModel, int>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.EReturnTemplate.DetailsViewModel, int>>();
        private readonly Mock<IModelBuilder<Models.EReturnTemplate.EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.EReturnTemplate.EditViewModel, int>>();
        private readonly Mock<IModelCommand<Models.EReturnTemplate.EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<Models.EReturnTemplate.EditViewModel>>();
        private readonly Mock<IModelCommand<Models.EReturnTemplate.EditViewModel>> _mockEditCommand = new Mock<IModelCommand<Models.EReturnTemplate.EditViewModel>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .Where(x => x.Name == "Details")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            var detailsViewModelBuilder = new Mock<IModelBuilder<Models.EReturnTemplate.DetailsViewModel, int>>();
            detailsViewModelBuilder.Setup(x => x.Build(It.IsAny<int>())).Returns(new Models.EReturnTemplate.DetailsViewModel());

            var dependencies = new EReturnTemplateControllerDependencies(
                _mockLogger.Object,
                detailsViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                _mockListViewModelBuilder.Object,
                _mockCreateCommand.Object,
                _mockEditCommand.Object);

            var controller = new EReturnTemplateController(dependencies);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["EReturnTemplateController::IsAPaymentSearch"]).Returns(false);

            controller.ControllerContext = controllerContext.Object;

            return controller.Details(1);
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

            Assert.AreEqual("eReturn Template Details", namedArgument.TypedValue.Value);
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
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Details");
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
            Assert.IsInstanceOfType(result.Model, typeof(Models.EReturnTemplate.DetailsViewModel));
        }
    }
}
