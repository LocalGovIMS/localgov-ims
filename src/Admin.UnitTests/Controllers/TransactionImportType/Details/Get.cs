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

namespace Admin.UnitTests.Controllers.TransactionImportType.Details
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(TransactionImportTypeController);
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.TransactionImportType.ListViewModel, Models.TransactionImportType.SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<Models.TransactionImportType.ListViewModel, Models.TransactionImportType.SearchCriteria>>();
        private readonly Mock<IModelBuilder<Models.TransactionImportType.DetailsViewModel, int>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.TransactionImportType.DetailsViewModel, int>>();
        private readonly Mock<IModelBuilder<Models.TransactionImportType.EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.TransactionImportType.EditViewModel, int>>();
        private readonly Mock<IModelCommand<Models.TransactionImportType.EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<Models.TransactionImportType.EditViewModel>>();
        private readonly Mock<IModelCommand<Models.TransactionImportType.EditViewModel>> _mockEditCommand = new Mock<IModelCommand<Models.TransactionImportType.EditViewModel>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .Where(x => x.Name == "Details")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            var detailsViewModelBuilder = new Mock<IModelBuilder<Models.TransactionImportType.DetailsViewModel, int>>();
            detailsViewModelBuilder.Setup(x => x.Build(It.IsAny<int>())).Returns(new Models.TransactionImportType.DetailsViewModel());

            var dependencies = new TransactionImportTypeControllerDependencies(
                _mockLogger.Object,
                detailsViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                _mockListViewModelBuilder.Object,
                _mockCreateCommand.Object,
                _mockEditCommand.Object);

            var controller = new TransactionImportTypeController(dependencies);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["TransactionImportTypeController::IsAPaymentSearch"]).Returns(false);

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

            Assert.AreEqual("Transaction Import Type Details", namedArgument.TypedValue.Value);
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
            Assert.IsInstanceOfType(result.Model, typeof(Models.TransactionImportType.DetailsViewModel));
        }
    }
}