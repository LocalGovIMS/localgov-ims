using Admin.Classes.ViewModelBuilders.MethodOfPaymentMetadata;
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
using Controller = Admin.Controllers.MethodOfPaymentMetadataController;
using ControllerDependencies = Admin.Controllers.MethodOfPaymentMetadataControllerDependencies;
using DetailsViewModel = Admin.Models.MethodOfPaymentMetadata.DetailsViewModel;
using EditViewModel = Admin.Models.MethodOfPaymentMetadata.EditViewModel;
using ListViewModel = Admin.Models.MethodOfPaymentMetadata.ListViewModel;
using SearchCriteria = Admin.Models.MethodOfPaymentMetadata.SearchCriteria;

namespace Admin.UnitTests.Controllers.MethodOfPaymentMetadata.EditListForImportProcessingRule
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<DetailsViewModel, int>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<DetailsViewModel, int>>();
        private readonly Mock<IModelBuilder<EditViewModel, CreateViewModelBuilderArgs>> _mockCreateViewModelBuilder = new Mock<IModelBuilder<EditViewModel, CreateViewModelBuilderArgs>>();
        private readonly Mock<IModelBuilder<EditViewModel, int>> _mockEditViewModelBuilder = new Mock<IModelBuilder<EditViewModel, int>>();
        private readonly Mock<IModelBuilder<ListViewModel, SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();
        private readonly Mock<IModelCommand<EditViewModel>> _mockCreateCommand = new Mock<IModelCommand<EditViewModel>>();
        private readonly Mock<IModelCommand<EditViewModel>> _mockEditCommand = new Mock<IModelCommand<EditViewModel>>();
        private readonly Mock<IModelCommand<int>> _mockDeleteCommand = new Mock<IModelCommand<int>>();

        private MethodInfo GetListMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.Name == "_EditListForMethodOfPayment")
                .FirstOrDefault();
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(2, GetListMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleChildActionOnlyAttribute()
        {
            Assert.AreEqual(1, GetListMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(ChildActionOnlyAttribute)).Count());
        }

        [TestMethod]
        public void HasASingleGetAttribute()
        {
            Assert.AreEqual(1, GetListMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpGetAttribute)).Count());
        }

        private ActionResult GetTestListResult()
        {
            var listViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();
            listViewModelBuilder.Setup(x => x.Build(It.IsAny<SearchCriteria>())).Returns(new ListViewModel());

            var dependencies = new ControllerDependencies(
                _mockLogger.Object,
                _mockDetailsViewModelBuilder.Object,
                _mockCreateViewModelBuilder.Object,
                _mockEditViewModelBuilder.Object,
                listViewModelBuilder.Object,
                _mockCreateCommand.Object,
                _mockEditCommand.Object,
                _mockDeleteCommand.Object);

            var controller = new Controller(dependencies);

            return controller._EditListForMethodOfPayment("1");
        }

        [TestMethod]
        public void ReturnsAPartialView()
        {
            var result = GetTestListResult();

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PartialViewResult));
        }

        [TestMethod]
        public void ReturnsCorrectViewName()
        {
            var result = GetTestListResult() as PartialViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "_EditList");
        }
    }
}
