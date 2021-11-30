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
using Controller = Admin.Controllers.TransactionController;
using Dependencies = Admin.Controllers.TransactionControllerDependencies;

namespace Admin.UnitTests.Controllers.Transaction.Refund
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Post
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<ListViewModel, SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();
        private readonly Mock<IModelBuilder<DetailsViewModel, string>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<DetailsViewModel, string>>();
        private readonly Mock<IModelBuilder<TransferViewModel, string>> _mockTransferViewModelBuilder = new Mock<IModelBuilder<TransferViewModel, string>>();
        private readonly Mock<IModelBuilder<RefundViewModel, string>> _mockRefundViewModelBuilder = new Mock<IModelBuilder<RefundViewModel, string>>();
        private readonly Mock<IModelCommand<TransferViewModel>> _mockTransferCommand = new Mock<IModelCommand<TransferViewModel>>();
        private readonly Mock<IModelCommand<string>> _mockUndoTransferCommand = new Mock<IModelCommand<string>>();
        private readonly Mock<IModelCommand<EmailReceiptViewModel>> _mockEmailReceiptCommand = new Mock<IModelCommand<EmailReceiptViewModel>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .Where(x => x.Name == "Refund")
                .FirstOrDefault();
        }

        private ActionResult GetResult(RefundViewModel model, bool success)
        {
            var refundCommand = new Mock<IModelCommand<RefundViewModel>>();

            refundCommand.Setup(x => x.Execute(
                It.IsAny<RefundViewModel>()))
                .Returns(new Admin.Classes.Commands.CommandResult(success, "TestMessage"));

            var dependencies = new Dependencies(
                _mockLogger.Object,
                _mockListViewModelBuilder.Object,
                _mockDetailsViewModelBuilder.Object,
                _mockTransferViewModelBuilder.Object,
                _mockRefundViewModelBuilder.Object,
                _mockTransferCommand.Object,
                _mockUndoTransferCommand.Object,
                refundCommand.Object,
                _mockEmailReceiptCommand.Object);

            var controller = new Controller(dependencies);

            return controller.Refund(model);
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
        public void ReturnsRedirectResult()
        {
            var result = GetResult(new RefundViewModel() { Reference = "Test" }, true);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void RedirectsToDetails()
        {
            var result = GetResult(new RefundViewModel() { Reference = "Test" }, true);

            Assert.IsNotNull(result);
            Assert.AreEqual("Details", ((RedirectToRouteResult)result).RouteValues["action"]);
        }

        [TestMethod]
        public void RedirectsToDetailsWithSameRefundReference()
        {
            var result = GetResult(new RefundViewModel() { Reference = "Test" }, true);

            Assert.IsNotNull(result);
            Assert.AreEqual("Test", ((RedirectToRouteResult)result).RouteValues["id"]);
        }
    }
}
