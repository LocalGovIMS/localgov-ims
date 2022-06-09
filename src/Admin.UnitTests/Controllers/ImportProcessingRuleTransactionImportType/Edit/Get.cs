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
using Web.Mvc.Navigation;
using Controller = Admin.Controllers.ImportProcessingRuleTransactionImportTypeController;
using ControllerDependencies = Admin.Controllers.ImportProcessingRuleTransactionImportTypeControllerDependencies;
using DetailsViewModel = Admin.Models.ImportProcessingRuleTransactionImportType.DetailsViewModel;
using EditViewModel = Admin.Models.ImportProcessingRuleTransactionImportType.EditViewModel;
using ListViewModel = Admin.Models.ImportProcessingRuleTransactionImportType.ListViewModel;
using SearchCriteria = Admin.Models.ImportProcessingRuleTransactionImportType.SearchCriteria;

namespace Admin.UnitTests.Controllers.ImportProcessingRuleTransactionImportType.Edit
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<DetailsViewModel, int>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<DetailsViewModel, int>>();
        private readonly Mock<IModelBuilder<EditViewModel, int>> _mockCreateViewModelBuilder = new Mock<IModelBuilder<EditViewModel, int>>();
        private readonly Mock<IModelBuilder<EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<EditViewModel, int>>();
        private readonly Mock<IModelBuilder<ListViewModel, SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();
        private readonly Mock<IModelCommand<EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<EditViewModel>>();
        private readonly Mock<IModelCommand<EditViewModel>> _mockEditCommand = new Mock<IModelCommand<EditViewModel>>();
        private readonly Mock<IModelCommand<int>> _mockDeleteCommand = new Mock<IModelCommand<int>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .Where(x => x.Name == "Edit")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            var editViewModelBuilder = new Mock<IModelBuilder<EditViewModel, int>>();
            editViewModelBuilder.Setup(x => x.Build(It.IsAny<int>())).Returns(new EditViewModel());

            var dependencies = new ControllerDependencies(
                _mockLogger.Object,
                _mockDetailsViewModelBuilder.Object,
                _mockCreateViewModelBuilder.Object,
                editViewModelBuilder.Object,
                _mockListViewModelBuilder.Object,
                _mockCreateCommand.Object,
                _mockEditCommand.Object,
                _mockDeleteCommand.Object);

            var controller = new Controller(dependencies);

            return controller.Edit(1);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(3, GetMethod().CustomAttributes.Count());
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

            Assert.AreEqual("Edit Transaction Import Type Link", namedArgument.TypedValue.Value);
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
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
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
            Assert.IsInstanceOfType(result.Model, typeof(EditViewModel));
        }
    }
}
