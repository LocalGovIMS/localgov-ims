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

namespace Admin.UnitTests.Controllers.TransactionImportType.List
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
                .Where(x => x.Name == "List")
                .FirstOrDefault();
        }

        private ActionResult GetResult()
        {
            var dependencies = new TransactionImportTypeControllerDependencies(
                _mockLogger.Object,
                _mockDetailsViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                _mockListViewModelBuilder.Object,
                _mockCreateCommand.Object,
                _mockEditCommand.Object);

            var controller = new TransactionImportTypeController(dependencies);

            return controller.List();
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(2, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleHttpGetAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpGetAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsViewResult()
        {
            var result = GetResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }
    }
}
