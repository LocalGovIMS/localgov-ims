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
using Controller = Admin.Controllers.AccountReferenceValidatorController;

namespace Admin.UnitTests.Controllers.AccountReferenceValidator.Create
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.AccountReferenceValidator.ListViewModel, Models.AccountReferenceValidator.SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<Models.AccountReferenceValidator.ListViewModel, Models.AccountReferenceValidator.SearchCriteria>>();
        private readonly Mock<IModelBuilder<Models.AccountReferenceValidator.DetailsViewModel, int>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.AccountReferenceValidator.DetailsViewModel, int>>();
        private readonly Mock<IModelBuilder<Models.AccountReferenceValidator.EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.AccountReferenceValidator.EditViewModel, int>>();
        private readonly Mock<IModelCommand<Models.AccountReferenceValidator.EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<Models.AccountReferenceValidator.EditViewModel>>();
        private readonly Mock<IModelCommand<Models.AccountReferenceValidator.EditViewModel>> _mockEditCommand = new Mock<IModelCommand<Models.AccountReferenceValidator.EditViewModel>>();
        private readonly Mock<IModelCommand<int>> _mockDeleteCommand = new Mock<IModelCommand<int>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .Where(x => x.Name == "Create")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            var editViewModelBuilder = new Mock<IModelBuilder<Models.AccountReferenceValidator.EditViewModel, int>>();
            editViewModelBuilder.Setup(x => x.Build()).Returns(new Models.AccountReferenceValidator.EditViewModel());

            var dependencies = new AccountReferenceValidatorControllerDependencies(
                _mockLogger.Object,
                _mockDetailsViewModelBuilder.Object,
                editViewModelBuilder.Object,
                _mockListViewModelBuilder.Object,
                _mockCreateCommand.Object,
                _mockEditCommand.Object,
                _mockDeleteCommand.Object);

            var controller = new Controller(dependencies);

            return controller.Create();
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

            Assert.AreEqual("Create Validator", namedArgument.TypedValue.Value);
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
            Assert.IsInstanceOfType(result.Model, typeof(Models.AccountReferenceValidator.EditViewModel));
        }
    }
}