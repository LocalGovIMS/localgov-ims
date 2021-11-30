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

namespace Admin.UnitTests.Controllers.Transaction.SubmitTransfers
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
        private readonly Mock<IModelCommand<string>> _mockUndoTransferCommand = new Mock<IModelCommand<string>>();
        private readonly Mock<IModelCommand<RefundViewModel>> _mockRefundCommand = new Mock<IModelCommand<RefundViewModel>>();
        private readonly Mock<IModelCommand<EmailReceiptViewModel>> _mockEmailReceiptCommand = new Mock<IModelCommand<EmailReceiptViewModel>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpPostAttribute)))
                .Where(x => x.Name == "SubmitTransfers")
                .FirstOrDefault();
        }

        private ActionResult GetResult(TransferViewModel model, bool throwException)
        {
            var transferCommand = new Mock<IModelCommand<TransferViewModel>>();

            transferCommand.Setup(x => x.Execute(
                It.IsAny<TransferViewModel>()))
                .Returns(new Admin.Classes.Commands.CommandResult(true, "TestMessage"));

            var dependencies = new Dependencies(
                _mockLogger.Object,
                _mockListViewModelBuilder.Object,
                _mockDetailsViewModelBuilder.Object,
                _mockTransferViewModelBuilder.Object,
                _mockRefundViewModelBuilder.Object,
                throwException ? null : transferCommand.Object,
                _mockUndoTransferCommand.Object,
                _mockRefundCommand.Object,
                _mockEmailReceiptCommand.Object);

            var controller = new Controller(dependencies);

            return controller.SubmitTransfers(model);
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
        public void ReturnsJsonResult()
        {
            var result = GetResult(null, false);

            Assert.IsInstanceOfType(result, typeof(JsonResult));
            Assert.AreEqual(((JsonResult)result).Data.ToString(), "{ ok = True, message = TestMessage }");
        }
    }
}
