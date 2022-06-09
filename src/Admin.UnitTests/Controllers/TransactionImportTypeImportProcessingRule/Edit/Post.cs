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
using Controller = Admin.Controllers.TransactionImportTypeImportProcessingRuleController;
using ControllerDependencies = Admin.Controllers.TransactionImportTypeImportProcessingRuleControllerDependencies;
using DetailsViewModel = Admin.Models.TransactionImportTypeImportProcessingRule.DetailsViewModel;
using EditViewModel = Admin.Models.TransactionImportTypeImportProcessingRule.EditViewModel;
using ListViewModel = Admin.Models.TransactionImportTypeImportProcessingRule.ListViewModel;
using SearchCriteria = Admin.Models.TransactionImportTypeImportProcessingRule.SearchCriteria;

namespace Admin.UnitTests.Controllers.TransactionImportTypeImportProcessingRule.Edit
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Post
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
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .Where(x => x.Name == "Edit")
                .FirstOrDefault();
        }

        private ActionResult GetResult(EditViewModel model, bool isModelValid)
        {
            var editCommand = new Mock<IModelCommand<EditViewModel>>();
            editCommand.Setup(x => x.Execute(It.IsAny<EditViewModel>())).Returns(new Admin.Classes.Commands.CommandResult(true));

            var mockEditViewModelBuilder = new Mock<IModelBuilder<EditViewModel, int>>();
            mockEditViewModelBuilder.Setup(x => x.Rebuild(It.IsAny<EditViewModel>())).Returns(model);
            mockEditViewModelBuilder.Setup(x => x.Build(It.IsAny<int>())).Returns(model);
            mockEditViewModelBuilder.Setup(x => x.Build()).Returns(model);

            var dependencies = new ControllerDependencies(
                _mockLogger.Object,
                _mockDetailsViewModelBuilder.Object,
                _mockCreateViewModelBuilder.Object,
                mockEditViewModelBuilder.Object,
                _mockListViewModelBuilder.Object,
                _mockCreateCommand.Object,
                editCommand.Object,
                _mockDeleteCommand.Object);

            var controller = new Controller(dependencies);

            if (!isModelValid)
            {
                controller.ModelState.AddModelError("ruleId", "error");
            }

            return controller.Edit(model);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(2, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpPostAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpPostAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsCorrectEditViewIfModelInvalid()
        {
            var result = GetResult(new EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Edit");
        }

        [TestMethod]
        public void ReturnsCorrectViewModelTypeIfModelInvalid()
        {
            var result = GetResult(new EditViewModel(), false) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(EditViewModel));
        }

        [TestMethod]
        public void ReturnsRedirectToRouteResultIfModelValid()
        {
            var result = GetResult(new EditViewModel(), true);

            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectToBackIfModelValid()
        {
            var result = GetResult(new EditViewModel(), true) as RedirectToRouteResult;

            Assert.AreEqual(result.RouteValues["action"], "Back");
        }

        [TestMethod]
        public void RedirectsToBackIfModelValid()
        {
            var result = GetResult(new EditViewModel(), true) as RedirectToRouteResult;

            Assert.IsNotNull(result);
        }
    }
}
