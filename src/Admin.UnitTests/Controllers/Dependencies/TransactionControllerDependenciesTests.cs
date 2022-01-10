using Admin.Classes.Commands.Transaction;
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
        private readonly Mock<IModelCommand<CreateCsvFileForExportCommandArgs>> _mockCreateCsvFileForExportCommand = new Mock<IModelCommand<CreateCsvFileForExportCommandArgs>>();

        [DataRow(false, true, true, true, true, true, true, true, true)]
        [DataRow(true, false, true, true, true, true, true, true, true)]
        [DataRow(true, true, false, true, true, true, true, true, true)]
        [DataRow(true, true, true, false, true, true, true, true, true)]
        [DataRow(true, true, true, true, false, true, true, true, true)]
        [DataRow(true, true, true, true, true, false, true, true, true)]
        [DataRow(true, true, true, true, true, true, false, true, true)]
        [DataRow(true, true, true, true, true, true, true, false, true)]
        [DataRow(true, true, true, true, true, true, true, true, false)]
        [TestMethod]
        public void ThrowsCorrectExceptionTypeWhenDependencyIsNull(
            bool includeListViewModelBuilder,
            bool includeDetailsViewModelBuilder,
            bool includeTransferViewModelBuilder,
            bool includeRefundViewModelBuilder,
            bool includeTransferCommand,
            bool includeUndoTransferCommand,
            bool includeRefundCommand,
            bool includeEmailReceiptCommand,
            bool includeCreateCsvFileForExportCommand)
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    includeListViewModelBuilder ? _mockListViewModelBuilder.Object : null,
                    includeDetailsViewModelBuilder ? _mockDetailsViewModelBuilder.Object : null,
                    includeTransferViewModelBuilder ? _mockTransferViewModelBuilder.Object : null,
                    includeRefundViewModelBuilder ? _mockRefundViewModelBuilder.Object : null,
                    includeTransferCommand ? _mockTransferCommand.Object : null,
                    includeUndoTransferCommand ? _mockUndoTransferCommand.Object : null,
                    includeRefundCommand ? _mockRefundCommand.Object : null,
                    includeEmailReceiptCommand ? _mockEmailReceiptCommand.Object : null,
                    includeCreateCsvFileForExportCommand ? _mockCreateCsvFileForExportCommand.Object : null);
            }
            catch (Exception e)
            {
                e.Should().BeOfType(typeof(ArgumentNullException));
            }
        }

        [DataRow(false, true, true, true, true, true, true, true, true, "listViewModelBuilder")]
        [DataRow(true, false, true, true, true, true, true, true, true, "detailsViewModelBuilder")]
        [DataRow(true, true, false, true, true, true, true, true, true, "transferViewModelBuilder")]
        [DataRow(true, true, true, false, true, true, true, true, true, "refundViewModelBuilder")]
        [DataRow(true, true, true, true, false, true, true, true, true, "transferCommand")]
        [DataRow(true, true, true, true, true, false, true, true, true, "undoTransferCommand")]
        [DataRow(true, true, true, true, true, true, false, true, true, "refundCommand")]
        [DataRow(true, true, true, true, true, true, true, false, true, "emailReceiptCommand")]
        [DataRow(true, true, true, true, true, true, true, true, false, "createCsvFileForExportCommand")]
        [TestMethod]
        public void ThrowsCorrectExceptionDescriptionWhenDependencyIsNull(
            bool includeListViewModelBuilder,
            bool includeDetailsViewModelBuilder,
            bool includeTransferViewModelBuilder,
            bool includeRefundViewModelBuilder,
            bool includeTransferCommand,
            bool includeUndoTransferCommand,
            bool includeRefundCommand,
            bool includeEmailReceiptCommand,
            bool includeCreateCsvFileForExportCommand,
            string parameterName)
        {
            try
            {
                var dependencies = new DependenciesClass(
                    _mockLogger.Object,
                    includeListViewModelBuilder ? _mockListViewModelBuilder.Object : null,
                    includeDetailsViewModelBuilder ? _mockDetailsViewModelBuilder.Object : null,
                    includeTransferViewModelBuilder ? _mockTransferViewModelBuilder.Object : null,
                    includeRefundViewModelBuilder ? _mockRefundViewModelBuilder.Object : null,
                    includeTransferCommand ? _mockTransferCommand.Object : null,
                    includeUndoTransferCommand ? _mockUndoTransferCommand.Object : null,
                    includeRefundCommand ? _mockRefundCommand.Object : null,
                    includeEmailReceiptCommand ? _mockEmailReceiptCommand.Object : null,
                    includeCreateCsvFileForExportCommand ? _mockCreateCsvFileForExportCommand.Object : null);
            }
            catch (ArgumentNullException exception)
            {
                exception.Message.Should().Be($"Value cannot be null.\r\nParameter name: {parameterName}");
            }
        }
    }
}
