using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models;
using log4net;
using System;

namespace Admin.Classes.Commands.FileImport
{
    public class ProcessCommand : BaseCommand<string>
    {
        private readonly IFileImportService _fileImportService;

        public ProcessCommand(ILog log
            , IFileImportService fileImportService
            )
            : base(log)
        {
            _fileImportService = fileImportService ?? throw new ArgumentNullException("fileImportService");
        }

        protected override CommandResult OnExecute(string batchReference)
        {
            try
            {
                var result = _fileImportService.Process(batchReference);

                if(!result.Success)
                {
                    return new CommandResult(false, result.Error)
                    {
                        Data = (ProcessResult)result.Data
                    };
                }

                return new CommandResult(true, "File processed successfully.")
                {
                    Data = (ProcessResult)result.Data
                };

            }
            catch (Exception ex)
            {
                return new CommandResult(false, ex.Message);
            }
        }
    }
}