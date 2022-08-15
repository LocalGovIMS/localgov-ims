using Api.Controllers.AccountHolders;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using MessagePack;
using System;
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
                EventLogs = GetImportEventLogs()
            };
        }

        public List<ImportRow> GetImportRows()
        {
            return Rows?
                .Select(x => new ImportRow()
                {
                    Data = Convert.ToBase64String(MessagePackSerializer.Serialize(x, MessagePack.Resolvers.ContractlessStandardResolver.Options))
                })
                .ToList();
        }

        private List<ImportEventLog> GetImportEventLogs()
        {
            return Errors?
                .Select(x => new ImportEventLog() { CreatedDate = System.DateTime.Now, Message = x, Type = (byte)ImportEventLogTypeEnum.Error })
                .ToList();
        }
    }
}