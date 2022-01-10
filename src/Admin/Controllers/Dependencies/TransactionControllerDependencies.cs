using Admin.Classes.Commands.Transaction;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Transaction;
using log4net;
using System;
using Unity;

namespace Admin.Controllers
{
    public class TransactionControllerDependencies : BaseControllerDependencies, ITransactionControllerDependencies
    {
        public TransactionControllerDependencies(ILog log
            , IModelBuilder<ListViewModel, SearchCriteria> listViewModelBuilder
            , IModelBuilder<DetailsViewModel, string> detailsViewModelBuilder
            , IModelBuilder<TransferViewModel, string> transferViewModelBuilder
            , IModelBuilder<RefundViewModel, string> refundViewModelBuilder
            , IModelCommand<TransferViewModel> transferCommand
            , [Dependency("UndoTransfer")] IModelCommand<string> undoTransferCommand
            , IModelCommand<RefundViewModel> refundCommand
            , IModelCommand<EmailReceiptViewModel> emailReceiptCommand
            , IModelCommand<CreateCsvFileForExportCommandArgs> createCsvFileForExportCommand
            )
            : base(log)
        {
            ListViewModelBuilder = listViewModelBuilder ?? throw new ArgumentNullException("listViewModelBuilder");
            DetailsViewModelBuilder = detailsViewModelBuilder ?? throw new ArgumentNullException("detailsViewModelBuilder");
            TransferViewModelBuilder = transferViewModelBuilder ?? throw new ArgumentNullException("transferViewModelBuilder");
            RefundViewModelBuilder = refundViewModelBuilder ?? throw new ArgumentNullException("refundViewModelBuilder");

            TransferCommand = transferCommand ?? throw new ArgumentNullException("transferCommand");
            UndoTransferCommand = undoTransferCommand ?? throw new ArgumentNullException("undoTransferCommand");
            RefundCommand = refundCommand ?? throw new ArgumentNullException("refundCommand");
            EmailReceiptCommand = emailReceiptCommand ?? throw new ArgumentNullException("emailReceiptCommand");
            CreateCsvFileForExportCommand = createCsvFileForExportCommand ?? throw new ArgumentNullException("createCsvFileForExportCommand");
        }

        public IModelBuilder<DetailsViewModel, string> DetailsViewModelBuilder { get; set; }
        public IModelBuilder<TransferViewModel, string> TransferViewModelBuilder { get; private set; }
        public IModelBuilder<RefundViewModel, string> RefundViewModelBuilder { get; set; }
        public IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; private set; }

        public IModelCommand<TransferViewModel> TransferCommand { get; private set; }
        public IModelCommand<string> UndoTransferCommand { get; private set; }
        public IModelCommand<RefundViewModel> RefundCommand { get; private set; }
        public IModelCommand<EmailReceiptViewModel> EmailReceiptCommand { get; private set; }
        public IModelCommand<CreateCsvFileForExportCommandArgs> CreateCsvFileForExportCommand { get; private set; }
    }
}