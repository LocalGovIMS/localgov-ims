using Admin.Interfaces.Commands;
using BusinessLogic.Models;

namespace Admin.Controllers
{
    public interface IValidationControllerDependencies : IBaseControllerDependencies
    {
        IModelCommand<TransferItem> ValidateTransferItemCommand { get; }
    }
}
