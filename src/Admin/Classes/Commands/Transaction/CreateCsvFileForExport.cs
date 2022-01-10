using BusinessLogic.Entities;
using BusinessLogic.Extensions;
using log4net;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace Admin.Classes.Commands.Transaction
{
    public class CreateCsvFileForExportCommand : BaseCommand<CreateCsvFileForExportCommandArgs>
    {
        public CreateCsvFileForExportCommand(ILog log)
            : base(log)
        {
        }

        protected override CommandResult OnExecute(CreateCsvFileForExportCommandArgs model)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < model.Transactions.Count; i++)
            {
                sb.Append(model.Transactions[i].ToExportString(","));
                sb.Append("\r\n");
            }

            return new CommandResult(true)
            {
                Data = new FileContentResult(Encoding.UTF8.GetBytes(sb.ToString()), "text/csv")
                {
                    FileDownloadName = "Export.csv"
                }
            };
        }
    }

    public class CreateCsvFileForExportCommandArgs
    {
        [JsonIgnore]
        public List<ProcessedTransaction> Transactions { get; set; }
    }
}