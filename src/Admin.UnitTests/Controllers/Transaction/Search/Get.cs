using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Transaction;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Web.Mvc.Navigation;
using Controller = Admin.Controllers.TransactionController;
using Dependencies = Admin.Controllers.TransactionControllerDependencies;

namespace Admin.UnitTests.Controllers.Transaction.Search
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<ListViewModel, SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();
        private readonly Mock<IModelBuilder<DetailsViewModel, string>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<DetailsViewModel, string>>();
        private readonly Mock<IModelBuilder<TransferViewModel, string>> _mockTransferViewModelBuilder = new Mock<IModelBuilder<TransferViewModel, string>>();
        private readonly Mock<IModelBuilder<RefundViewModel, string>> _mockRefundViewModelBuilder = new Mock<IModelBuilder<RefundViewModel, string>>();
        private readonly Mock<IModelCommand<TransferViewModel>> _mockTransferCommand = new Mock<IModelCommand<TransferViewModel>>();
        private readonly Mock<IModelCommand<string>> _mockUndoTransferCommand = new Mock<IModelCommand<string>>();
        private readonly Mock<IModelCommand<RefundViewModel>> _mockRefundCommand = new Mock<IModelCommand<RefundViewModel>>();
        private readonly Mock<IModelCommand<EmailReceiptViewModel>> _mockEmailReceiptCommand = new Mock<IModelCommand<EmailReceiptViewModel>>();

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
                _mockListViewModelBuilder.Object,
                _mockDetailsViewModelBuilder.Object,
                _mockTransferViewModelBuilder.Object,
                _mockRefundViewModelBuilder.Object,
                _mockTransferCommand.Object,
                _mockUndoTransferCommand.Object,
                _mockRefundCommand.Object,
                _mockEmailReceiptCommand.Object);

            var controller = new Controller(dependencies);

            return controller.Search(model);
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(3, GetMethod().CustomAttributes.Count());
        }

        [TestMethod]
        public void HasASingleAcceptVerbsAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(AcceptVerbsAttribute)).Count());
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

            Assert.AreEqual("Transactions", namedArgument.TypedValue.Value);
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
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "~/Views/Transaction/List.cshtml");
        }
    }
}
