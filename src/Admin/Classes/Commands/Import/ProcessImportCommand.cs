using BusinessLogic.Interfaces.Services;
using BusinessLogic.Models;
using log4net;
using System;

namespace Admin.Classes.Commands.Import
{
    public class ProcessImportCommand : BaseCommand<string>
    {
        private readonly IImportService _importService;

        public ProcessImportCommand(ILog log
            , IImportService importService
            )
            : base(log)
        {
            _importService = importService ?? throw new ArgumentNullException("importService");
        }

        protected override CommandResult OnExecute(string batchReference)
        {
            try
            {
                var result = _importService.Process(batchReference);

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