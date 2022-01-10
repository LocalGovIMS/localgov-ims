using Admin.Classes.Commands.Transaction;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Transaction;
using log4net;
using Moq;
using Controller = Admin.Controllers.TransactionController;
using Dependencies = Admin.Controllers.TransactionControllerDependencies;

namespace Admin.UnitTests.Controllers.Transaction
{
    public class TestBase
    {
        protected Controller Controller;

        protected Mock<ILog> MockLogger = new Mock<ILog>();
        protected Mock<IModelBuilder<ListViewModel, SearchCriteria>> MockListViewModelBuilder = new Mock<IModelBuilder<ListViewModel, SearchCriteria>>();
        protected Mock<IModelBuilder<DetailsViewModel, string>> MockDetailsViewModelBuilder = new Mock<IModelBuilder<DetailsViewModel, string>>();
        protected Mock<IModelBuilder<TransferViewModel, string>> MockTransferViewModelBuilder = new Mock<IModelBuilder<TransferViewModel, string>>();
        protected Mock<IModelBuilder<RefundViewModel, string>> MockRefundViewModelBuilder = new Mock<IModelBuilder<RefundViewModel, string>>();
        protected Mock<IModelCommand<TransferViewModel>> MockTransferCommand = new Mock<IModelCommand<TransferViewModel>>();
        protected Mock<IModelCommand<string>> MockUndoTransferCommand = new Mock<IModelCommand<string>>();
        protected Mock<IModelCommand<RefundViewModel>> MockRefundCommand = new Mock<IModelCommand<RefundViewModel>>();
        protected Mock<IModelCommand<EmailReceiptViewModel>> MockEmailReceiptCommand = new Mock<IModelCommand<EmailReceiptViewModel>>();
        protected Mock<IModelCommand<CreateCsvFileForExportCommandArgs>> MockCreateCsvFileForExportCommand = new Mock<IModelCommand<CreateCsvFileForExportCommandArgs>>();

        protected void SetupController()
        {
            var dependencies = new Dependencies(
                    MockLogger.Object,
                    MockListViewModelBuilder.Object,
                    MockDetailsViewModelBuilder.Object,
                    MockTransferViewModelBuilder.Object,
                    MockRefundViewModelBuilder.Object,
                    MockTransferCommand.Object,
                    MockUndoTransferCommand.Object,
                    MockRefundCommand.Object,
                    MockEmailReceiptCommand.Object,
                    MockCreateCsvFileForExportCommand.Object);

            Controller = new Controller(dependencies);
        }
    }
}
