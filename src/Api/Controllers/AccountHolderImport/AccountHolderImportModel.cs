using Api.Controllers.AccountHolders;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers.AccountHolderImport
{
    public class AccountHolderImportModel
    {
        public int ImportTypeId { get; set; }
        public string Notes { get; set; }
        public int NumberOfRows { get; set; }
        public List<AccountHolderModel> Rows { get; set; }
        public List<string> Errors { get; set; }

        public AccountHolderImportModel() { }

        public Import GetImport()
        {
            return new Import()
            {
                ImportTypeId = ImportTypeId,
                Notes = Notes,
                NumberOfRows = NumberOfRows,
                Rows = GetImportRows(),  
                EventLogs = GetImportEventLogs()
            }; 
        }

        private List<ImportRow> GetImportRows()
        {
            return Rows?.Select(x => new ImportRow() { Data = Newtonsoft.Json.JsonConvert.SerializeObject(x.GetAccountHolder()) })
                .ToList();
        }

        private List<ImportEventLog> GetImportEventLogs()
        {
            return Errors?.Select(x => new ImportEventLog() { CreatedDate = System.DateTime.Now, Message = x, Type = (byte)ImportEventLogTypeEnum.Error })
                .ToList();
        }
    }
}