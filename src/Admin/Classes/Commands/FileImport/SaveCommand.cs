using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models.Import;
using log4net;
using Newtonsoft.Json;
using System;
using System.IO.Abstractions;
using System.Web;

namespace Admin.Classes.Commands.FileImport
{
    public class SaveCommand : BaseCommand<SaveCommandArgs>
    {
        private readonly IFileImportService _fileImportService;
        private readonly IFileSystem _fileSystem;

        private string _filename;
        private LoadFromFileResult _loadFromFileResult;

        public SaveCommand(ILog log
            , IFileImportService fileImportService
            , IFileSystem fileSystem)
            : base(log)
        {
            _fileImportService = fileImportService ?? throw new ArgumentNullException("fileImportService");
            _fileSystem = fileSystem ?? throw new ArgumentNullException("fileSystem");
        }

        protected override CommandResult OnExecute(SaveCommandArgs args)
        {
            try
            {
                Validate(args);

                EnsureDirectoryToSaveFileInExists(args);

                SaveFile(args);

                CreateImportRecords(args);

                var result = new CommandResult(true, "File uploaded successfully.")
                {
                    Data = _loadFromFileResult
                };

                return result;
            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }

        private void Validate(SaveCommandArgs args)
        {
            if (args.File == null)
                throw new InvalidOperationException("Unable to locate file");

            if (args.Path == null)
                throw new InvalidOperationException("The path to save the file to is not specified");
        }

        private void EnsureDirectoryToSaveFileInExists(SaveCommandArgs args)
        {
            if (!_fileSystem.Directory.Exists(args.Path))
            {
                _fileSystem.Directory.CreateDirectory(args.Path);
            }
        }

        private void SaveFile(SaveCommandArgs args)
        {
            _filename = Guid.NewGuid().ToString();
            args.File.SaveAs(args.Path + _filename + _fileSystem.Path.GetExtension(args.File.FileName));
        }

        private void CreateImportRecords(SaveCommandArgs args)
        {
            var result = _fileImportService.LoadFromFile(args.Path + _filename + _fileSystem.Path.GetExtension(args.File.FileName));

            if (!result.Success)
                throw new InvalidOperationException(result.Error);

            if (result.Data == null)
                throw new NullReferenceException("File import result is unknown");

            if (!(result.Data is LoadFromFileResult))
                throw new NullReferenceException("File import result is unknown");

            _loadFromFileResult = (LoadFromFileResult)result.Data;
        }
    }

    public class SaveCommandArgs
    {
        [JsonIgnore]
        public HttpPostedFileBase File { get; set; }
        public string Path { get; set; }
    }
}