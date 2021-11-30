using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Transaction;
using FluentAssertions;
using log4net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Diagnostics.CodeAnalysis;
using DependenciesClass = Admin.Controllers.TransactionControllerDependencies;

namespace Admin.UnitTests.Classes.Dependencies
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class TransactionControllerDependenciesTests
    {

        private readonly Mock<ILog> _mockLogger = new Mock<ILog>();
        private readonly Mock<IModelBuilder<ListViewModel, SearchCriteria>> _mockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();
        private readonly Mock<IModelBuilder<DetailsViewModel, string>> _mockDetailsViewModelBuilder = new Mock<IModelBuilder<DetailsViewModel, string>>();
        private readonly Mock<IModelBuilder<TransferViewModel, string>> _mockTransferViewModelBuilder = new Mock<IModelBuilder<TransferViewModel, string>>();
        private readonly Mock<IModelBuilder<RefundViewModel, string>> _mockRefundViewModelBuilder = new Mock<IModelBuilder<RefundViewModel, string>>();
        private readonly Mock<IModelCommand<TransferViewModel>> _mockTransferCommand = new Mock<IModelCommand<TransferViewModel>>();
        private readonly Mock<IModelCommand<string>> _mockUndoTransferCommand = new Mock<IModelCommand<string>>();
        private readonly Mock<IModelCommand<RefundViewModel>> _mockRefundCommand = new Mock<IModelCommand<RefundViewModel>>();
        private readonly Mock<IModelCommand<EmailReceiptViewModel>> _mockEmailReceiptCommand = new Mock<IModelCommand<EmailReceiptViewModel>>();

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenListViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null,
                    _mockDetailsViewModelBuilder.Object,
                    _mockTransferViewModelBuilder.Object,
                    _mockRefundViewModelBuilder.Object,
                    _mockTransferCommand.Object,
                    _mockUndoTransferCommand.Object,
                    _mockRefundCommand.Object,
                    _mockEmailReceiptCommand.Object);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenListViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    null,
                    _mockDetailsViewModelBuilder.Object,
                    _mockTransferViewModelBuilder.Object,
                    _mockRefundViewModelBuilder.Object,
                    _mockTransferCommand.Object,
                    _mockUndoTransferCommand.Object,
                    _mockRefundCommand.Object,
                    _mockEmailReceiptCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: listViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenDetailsViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    null,
                    _mockTransferViewModelBuilder.Object,
                    _mockRefundViewModelBuilder.Object,
                    _mockTransferCommand.Object,
                    _mockUndoTransferCommand.Object,
                    _mockRefundCommand.Object,
                    _mockEmailReceiptCommand.Object);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenDetailsViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    null,
                    _mockTransferViewModelBuilder.Object,
                    _mockRefundViewModelBuilder.Object,
                    _mockTransferCommand.Object,
                    _mockUndoTransferCommand.Object,
                    _mockRefundCommand.Object,
                    _mockEmailReceiptCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: detailsViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenTransferViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    null,
                    _mockRefundViewModelBuilder.Object,
                    _mockTransferCommand.Object,
                    _mockUndoTransferCommand.Object,
                    _mockRefundCommand.Object,
                    _mockEmailReceiptCommand.Object);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenTransferViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    null,
                    _mockRefundViewModelBuilder.Object,
                    _mockTransferCommand.Object,
                    _mockUndoTransferCommand.Object,
                    _mockRefundCommand.Object,
                    _mockEmailReceiptCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: transferViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenRefundViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockTransferViewModelBuilder.Object,
                    null,
                    _mockTransferCommand.Object,
                    _mockUndoTransferCommand.Object,
                    _mockRefundCommand.Object,
                    _mockEmailReceiptCommand.Object);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenRefundViewModelBuiderIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockTransferViewModelBuilder.Object,
                    null,
                    _mockTransferCommand.Object,
                    _mockUndoTransferCommand.Object,
                    _mockRefundCommand.Object,
                    _mockEmailReceiptCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: refundViewModelBuilder");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenTransferCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockTransferViewModelBuilder.Object,
                    _mockRefundViewModelBuilder.Object,
                    null,
                    _mockUndoTransferCommand.Object,
                    _mockRefundCommand.Object,
                    _mockEmailReceiptCommand.Object);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenTransferCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockTransferViewModelBuilder.Object,
                    _mockRefundViewModelBuilder.Object,
                    null,
                    _mockUndoTransferCommand.Object,
                    _mockRefundCommand.Object,
                    _mockEmailReceiptCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: transferCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenUndoTransferCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockTransferViewModelBuilder.Object,
                    _mockRefundViewModelBuilder.Object,
                    _mockTransferCommand.Object,
                    null,
                    _mockRefundCommand.Object,
                    _mockEmailReceiptCommand.Object);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenUndoTransferCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockTransferViewModelBuilder.Object,
                    _mockRefundViewModelBuilder.Object,
                    _mockTransferCommand.Object,
                    null,
                    _mockRefundCommand.Object,
                    _mockEmailReceiptCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: undoTransferCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenRefundCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockTransferViewModelBuilder.Object,
                    _mockRefundViewModelBuilder.Object,
                    _mockTransferCommand.Object,
                    _mockUndoTransferCommand.Object,
                    null,
                    _mockEmailReceiptCommand.Object);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenRefundCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockTransferViewModelBuilder.Object,
                    _mockRefundViewModelBuilder.Object,
                    _mockTransferCommand.Object,
                    _mockUndoTransferCommand.Object,
                    null,
                    _mockEmailReceiptCommand.Object);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: refundCommand");
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenEmailReceiptCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockTransferViewModelBuilder.Object,
                    _mockRefundViewModelBuilder.Object,
                    _mockTransferCommand.Object,
                    _mockUndoTransferCommand.Object,
                    _mockRefundCommand.Object,
                    null);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionTypeWhenEmailReceiptCommandIsNull()
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockTransferViewModelBuilder.Object,
                    _mockRefundViewModelBuilder.Object,
                    _mockTransferCommand.Object,
                    _mockUndoTransferCommand.Object,
                    _mockRefundCommand.Object,
                    null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be("Value cannot be null.\r\nParameter name: emailReceiptCommand");
            }
        }

        [TestMethod]
        public void CanSetProperties()
        {
            var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    _mockListViewModelBuilder.Object,
                    _mockDetailsViewModelBuilder.Object,
                    _mockTransferViewModelBuilder.Object,
                    _mockRefundViewModelBuilder.Object,
                    _mockTransferCommand.Object,
                    _mockUndoTransferCommand.Object,
                    _mockRefundCommand.Object,
                    _mockEmailReceiptCommand.Object);

            Assert.IsNotNull(dependencies.ListViewModelBuilder);
            Assert.IsNotNull(dependencies.DetailsViewModelBuilder);
            Assert.IsNotNull(dependencies.TransferViewModelBuilder);
            Assert.IsNotNull(dependencies.RefundViewModelBuilder);
            Assert.IsNotNull(dependencies.TransferCommand);
            Assert.IsNotNull(dependencies.UndoTransferCommand);
            Assert.IsNotNull(dependencies.RefundCommand);
            Assert.IsNotNull(dependencies.EmailReceiptCommand);
        }
    }
}
