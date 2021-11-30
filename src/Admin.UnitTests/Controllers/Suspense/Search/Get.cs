﻿using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Suspense;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Web.Mvc.Navigation;
using Controller = Admin.Controllers.SuspenseController;
using Dependencies = Admin.Controllers.SuspenseControllerDependencies;

namespace Admin.UnitTests.Controllers.Suspense.Search
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<ListViewModel, SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();
        private readonly Mock<IModelBuilder<DetailsViewModel, int>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<DetailsViewModel, int>>();
        private readonly Mock<IModelCommand<JournalViewModel>> _mockJournalCommand = new Mock<IModelCommand<JournalViewModel>>();
        private readonly Mock<IModelCommand<SaveNoteViewModel>> _mockSaveNoteCommand = new Mock<IModelCommand<SaveNoteViewModel>>();
        private readonly Mock<IModelBuilder<JournalViewModel, string>> _mockJournalModelViewBuilder = new Mock<IModelBuilder<JournalViewModel, string>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.Name == "Search")
                .FirstOrDefault();
        }

        private ActionResult GetResult(SearchCriteria model)
        {
            var basicListViewModelBuilder = new Mock<IModelBuilder<Models.Shared.BasicListViewModel, int>>();
            basicListViewModelBuilder.Setup(x => x.Build(It.IsAny<int>())).Returns(new Models.Shared.BasicListViewModel());

            var dependencies = new Dependencies(
                _mockLogger.Object,
                _mockDetailsViewModelBuilder.Object,
                _mockListViewModelBuilder.Object,
                _mockJournalModelViewBuilder.Object,
                _mockJournalCommand.Object,
                _mockSaveNoteCommand.Object
            );

            var controller = new Controller(dependencies);

            return controller.Search(model);
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
        public void ReturnsAView()
        {
            var result = GetResult(null);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void ReturnsCorrectViewName()
        {
            var result = GetResult(null) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "List");
        }
    }
}
