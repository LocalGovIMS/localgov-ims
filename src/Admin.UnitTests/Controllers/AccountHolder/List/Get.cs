﻿using Admin.Classes.ViewModelBuilders.AccountHolder;
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

namespace Admin.UnitTests.Controllers.AccountHolder.List
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(AccountHolderController);
        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<Models.AccountHolder.ListViewModel, Models.AccountHolder.SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<Models.AccountHolder.ListViewModel, Models.AccountHolder.SearchCriteria>>();
        private readonly Mock<IModelBuilder<Models.AccountHolder.DetailsViewModel, DetailsViewModelBuilderArgs>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<Models.AccountHolder.DetailsViewModel, DetailsViewModelBuilderArgs>>();
        private readonly Mock<IModelBuilder<Models.AccountHolder.EditViewModel, string>> _mockEditViewModelBuilder = new Mock<IModelBuilder<Models.AccountHolder.EditViewModel, string>>();
        private readonly Mock<IModelCommand<Models.AccountHolder.LookupViewModel>> _mockLookupAccountHolderCommand = new Mock<IModelCommand<Models.AccountHolder.LookupViewModel>>();
        private readonly Mock<IModelCommand<Models.AccountHolder.EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<Models.AccountHolder.EditViewModel>>();
        private readonly Mock<IModelCommand<Models.AccountHolder.EditViewModel>> _mockEditCommand = new Mock<IModelCommand<Models.AccountHolder.EditViewModel>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .Where(x => x.Name == "List")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            var dependencies = new AccountHolderControllerDependencies(
                _mockLogger.Object,
                _mockListViewModelBuilder.Object,
                _mockDetailsViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                _mockLookupAccountHolderCommand.Object,
                _mockCreateCommand.Object,
                _mockEditCommand.Object);

            var controller = new AccountHolderController(dependencies);

            return controller.List();
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(3, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpGetAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpGetAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsRedirectResult()
        {
            var result = GetResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectsToSearch()
        {
            var result = GetResult();

            Assert.IsNotNull(result);
            Assert.AreEqual("Search", ((RedirectToRouteResult)result).RouteValues["action"]);
        }

    }
}
