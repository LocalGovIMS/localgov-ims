using Admin.Classes.Commands.FileImport;
using Admin.Interfaces.Commands;
using log4net;
using System;
using Unity;

namespace Admin.Controllers
{
    public class FileImportControllerDependencies : BaseControllerDependencies, IFileImportControllerDependencies
    {
        public FileImportControllerDependencies(
            ILog log
            , IModelCommand<SaveCommandArgs> saveCommand
            , [Dependency("FileImport.Command.Process")]IModelCommand<int> processCommand)
             : base(log)
        {
            SaveCommand = saveCommand ?? throw new ArgumentNullException("saveCommand");
            ProcessCommand = processCommand ?? throw new ArgumentNullException("processCommand");
        }

        public IModelCommand<SaveCommandArgs> SaveCommand { get; private set; }
        public IModelCommand<int> ProcessCommand { get; private set; }
    }
}