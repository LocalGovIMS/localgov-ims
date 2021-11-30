using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Transfer;
using BusinessLogic.Models;
using log4net;

namespace Admin.Controllers
{
    public class TransferControllerDependencies : BaseControllerDependencies, ITransferControllerDependencies
    {
        public TransferControllerDependencies(ILog log
            , IModelBuilder<TransferViewModel, string> transferViewModelBuilder
            , IModelCommand<TransferViewModel> transferCommand
            , IModelCommand<TransferItem> validateTransferItemCommand)
            : base(log)
        {
            ValidateTransferItemCommand = validateTransferItemCommand;
            TransferViewModelBuilder = transferViewModelBuilder;
            TransferCommand = transferCommand;
        }

        public IModelBuilder<TransferViewModel, string> TransferViewModelBuilder { get; private set; }
        public IModelCommand<TransferViewModel> TransferCommand { get; private set; }
        public IModelCommand<TransferItem> ValidateTransferItemCommand { get; private set; }
    }
}