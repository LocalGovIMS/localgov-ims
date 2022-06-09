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

namespace Admin.UnitTests.Controllers.TransactionImportType.Search
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
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(AcceptVerbsAttribute)))
                .Where(x => x.Name == "Search")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            var listViewModelBuilder = new Mock<IModelBuilder<Models.TransactionImportType.ListViewModel, Models.TransactionImportType.SearchCriteria>>();
            listViewModelBuilder.Setup(x => x.Build(It.IsAny<Models.TransactionImportType.SearchCriteria>())).Returns(new Models.TransactionImportType.ListViewModel());

            var dependencies = new TransactionImportTypeControllerDependencies(
                _mockLogger.Object,
                _mockDetailsViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                listViewModelBuilder.Object,
                _mockCreateCommand.Object,
                _mockEditCommand.Object);

            var controller = new TransactionImportTypeController(dependencies);

            var controllerContext = new Mock<ControllerContext>();
            controllerContext.SetupGet(p => p.HttpContext.Session["TransactionImportTypeController::IsAPaymentSearch"]).Returns(false);

            controller.ControllerContext = controllerContext.Object;

            return controller.Search(new Models.TransactionImportType.SearchCriteria());
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(2, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleAcceptVerbsAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(AcceptVerbsAttribute)).Count());
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
            Assert.IsInstanceOfType(result.Model, typeof(Models.TransactionImportType.ListViewModel));
        }
    }
}
