using Admin.Classes.Commands.Import;
using Admin.Interfaces.Commands;

namespace Admin.Controllers
{
    public interface IImportControllerDependencies : IBaseControllerDependencies
    {
        IModelCommand<SaveImportFileCommandArgs> SaveImportFileCommand { get; }
        IModelCommand<string> ProcessImportCommand { get; }
    }
}
