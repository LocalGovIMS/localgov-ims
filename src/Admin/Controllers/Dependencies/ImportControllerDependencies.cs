using Admin.Classes.Commands.Import;
using Admin.Interfaces.Commands;
using log4net;
using System;
using Unity;

namespace Admin.Controllers
{
    public class ImportControllerDependencies : BaseControllerDependencies, IImportControllerDependencies
    {
        public ImportControllerDependencies(
            ILog log
            , IModelCommand<SaveImportFileCommandArgs> saveImportFileCommand
            , [Dependency("Import.Command.ProcessImport")]IModelCommand<string> processImportCommand)
             : base(log)
        {
            SaveImportFileCommand = saveImportFileCommand ?? throw new ArgumentNullException("saveImportFileCommand");
            ProcessImportCommand = processImportCommand ?? throw new ArgumentNullException("processImportCommand");
        }

        public IModelCommand<SaveImportFileCommandArgs> SaveImportFileCommand { get; private set; }
        public IModelCommand<string> ProcessImportCommand { get; private set; }
    }
}