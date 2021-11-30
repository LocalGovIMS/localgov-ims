using Admin.Interfaces.Commands;
using Admin.Interfaces.ModelBuilders;
using Admin.Models.Transfer;
using BusinessLogic.Models;

namespace Admin.Controllers
{
    public interface ITransferControllerDependencies : IBaseControllerDependencies
    {
        IModelBuilder<TransferViewModel, string> TransferViewModelBuilder { get; }

        IModelCommand<TransferViewModel> TransferCommand { get; }

        IModelCommand<TransferItem> ValidateTransferItemCommand { get; }

    }
}
