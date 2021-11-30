using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Transaction;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;
using Web.Mvc.Navigation;
using Controller = Admin.Controllers.TransactionController;
using Dependencies = Admin.Controllers.TransactionControllerDependencies;

namespace Admin.UnitTests.Controllers.Transaction.Details
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class Get
    {
        private readonly Type _controller = typeof(Controller);

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<ListViewModel, SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();
        private readonly Mock<IModelBuilder<TransferViewModel, string>> _mockTransferViewModelBuilder = new Mock<IModelBuilder<TransferViewModel, string>>();
        private readonly Mock<IModelBuilder<RefundViewModel, string>> _mockRefundViewModelBuilder = new Mock<IModelBuilder<RefundViewModel, string>>();
        private readonly Mock<IModelCommand<TransferViewModel>> _mockTransferCommand = new Mock<IModelCommand<TransferViewModel>>();
        private readonly Mock<IModelCommand<string>> _mockUndoTransferCommand = new Mock<IModelCommand<string>>();
        private readonly Mock<IModelCommand<RefundViewModel>> _mockRefundCommand = new Mock<IModelCommand<RefundViewModel>>();
        private readonly Mock<IModelCommand<EmailReceiptViewModel>> _mockEmailReceiptCommand = new Mock<IModelCommand<EmailReceiptViewModel>>();

        private MethodInfo GetMethod()
        {
            return _controller.GetMethods()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(HttpGetAttribute)))
                .Where(x => x.Name == "Details")
                .FirstOrDefault();
        }

        private ActionResult GetResult(bool includeTransaction)
        {

            var processedTransactions = new List<BusinessLogic.Entities.ProcessedTransaction>()
            {
                {
                    new BusinessLogic.Entities.ProcessedTransaction()
                    {
                        TransactionReference = "12345",
                        AccountReference = "ABCDE",
                        FundCode = "F1",
                        Amount = 100,
                        MopCode = "90",
                        Mop = new BusinessLogic.Entities.Mop()
                        {
                            MopCode = "90",
                            MetaData = new List<BusinessLogic.Entities.MopMetaData>()
                        }
                    }
                }
            };

            var pendingTransactions = new List<BusinessLogic.Entities.PendingTransaction>();
            var failedRefunds = new List<BusinessLogic.Entities.PendingTransaction>();

            var processedRefunds = new List<BusinessLogic.Entities.ProcessedTransaction>()
            {
                {
                    new BusinessLogic.Entities.ProcessedTransaction()
                    {
                        TransactionReference = "12345",
                        AccountReference = "ABCDE",
                        FundCode = "F1",
                        Amount = 1
                    }
                }
            };

            var transfers = new List<BusinessLogic.Entities.ProcessedTransaction>();

            var detailsViewModelBuilder = new Mock<IModelBuilder<DetailsViewModel, string>>();
            detailsViewModelBuilder.Setup(x => x.Build(
                It.IsAny<string>()))
                .Returns(new DetailsViewModel()
                {
                    Transaction = includeTransaction
                    ? new BusinessLogic.Models.Transaction(
                        processedTransactions,
                        pendingTransactions,
                        failedRefunds,
                        processedRefunds,
                        transfers,
                        string.Empty)
                    : null
                }
                );

            var dependencies = new Dependencies(
                _mockLogger.Object,
                _mockListViewModelBuilder.Object,
                detailsViewModelBuilder.Object,
                _mockTransferViewModelBuilder.Object,
                _mockRefundViewModelBuilder.Object,
                _mockTransferCommand.Object,
                _mockUndoTransferCommand.Object,
                _mockRefundCommand.Object,
                _mockEmailReceiptCommand.Object);

            var controller = new Controller(dependencies);

            return controller.Details("test");
        }

        [TestMethod]
        public void HasCorrectNumberOfCustomAttributes()
        {
            Assert.AreEqual(3, GetMethod().CustomAttributes.Count());
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

            Assert.AreEqual("Transaction Details", namedArgument.TypedValue.Value);
        }

        [TestMethod]
        public void HasASingleHttpGetAttribute()
        {
            Assert.AreEqual(1, GetMethod().CustomAttributes.Where(ca => ca.AttributeType == typeof(HttpGetAttribute)).Count());
        }

        [TestMethod]
        public void ReturnsAView()
        {
            var result = GetResult(true);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(ViewResult));
        }

        [TestMethod]
        public void ReturnsCorrectViewName()
        {
            var result = GetResult(true) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsTrue(string.IsNullOrEmpty(result.ViewName) || result.ViewName == "Details");
        }

        [TestMethod]
        public void ReturnsAViewModel()
        {
            var result = GetResult(true) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Model);
        }

        [TestMethod]
        public void ReturnsCorrectViewModelType()
        {
            var result = GetResult(true) as ViewResult;

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result.Model, typeof(DetailsViewModel));
        }

        [TestMethod]
        public void NoTransactionReturnsRedirectResult()
        {
            var result = GetResult(false);

            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(RedirectToRouteResult));
        }

        [TestMethod]
        public void NoTransactionRedirectsToList()
        {
            var result = GetResult(false);

            Assert.IsNotNull(result);
            Assert.AreEqual("List", ((RedirectToRouteResult)result).RouteValues["action"]);
        }
    }
}
