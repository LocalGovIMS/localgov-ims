using Admin.Classes.Commands.Transaction;
using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Transaction;
using DetailsViewModel = Admin.Models.Transaction.DetailsViewModel;

namespace Admin.Controllers
{
    public interface ITransactionControllerDependencies : IBaseControllerDependencies
    {
        #region ModelBuilders
        IModelBuilder<ListViewModel, SearchCriteria> ListViewModelBuilder { get; }
        IModelBuilder<DetailsViewModel, string> DetailsViewModelBuilder { get; }
        IModelBuilder<TransferViewModel, string> TransferViewModelBuilder { get; }
        IModelBuilder<RefundViewModel, string> RefundViewModelBuilder { get; }
        #endregion

        #region ModelCommands        
        IModelCommand<TransferViewModel> TransferCommand { get; }
        IModelCommand<string> UndoTransferCommand { get; }
        IModelCommand<RefundViewModel> RefundCommand { get; }
        IModelCommand<EmailReceiptViewModel> EmailReceiptCommand { get; }
        IModelCommand<CreateCsvFileForExportCommandArgs> CreateCsvFileForExportCommand { get; }
        #endregion
    }
}