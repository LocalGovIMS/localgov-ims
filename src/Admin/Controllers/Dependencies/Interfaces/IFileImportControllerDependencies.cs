using Admin.Classes.Commands.FileImport;
using Admin.Interfaces.Commands;

namespace Admin.Controllers
{
    public interface IFileImportControllerDependencies : IBaseControllerDependencies
    {
        IModelCommand<SaveCommandArgs> SaveCommand { get; }
        IModelCommand<int> ProcessCommand { get; }
    }
}
