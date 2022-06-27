using Api.Controllers.ProcessedTransactions;
using BusinessLogic.Entities;
using BusinessLogic.Enums;
using System.Collections.Generic;
using System.Linq;

namespace Api.Controllers.TransactionImport
{
    public class TransactionImportModel
    {
        public int TransactionImportTypeId { get; set; }
        public string ExternalReference { get; set; }
        public string Description { get; set; }
        public decimal TotalAmount { get; set; }
        public decimal TotalNumberOfTransactions { get; set; }
        public List<ProcessedTransactionModel> Rows { get; set; }
        public List<string> Errors { get; set; }

        public TransactionImportModel() { }

        public BusinessLogic.Entities.TransactionImport GetTransactionImport()
        {
            return new BusinessLogic.Entities.TransactionImport()
            {
                TransactionImportTypeId = TransactionImportTypeId,
                ExternalReference = ExternalReference,
                Description = Description,
                TotalAmount = TotalAmount,
                TotalNumberOfTransactions = TotalNumberOfTransactions,
                Rows = GetTransactionImportRows(),  
                EventLogs = GetTransactionImportEventLogs()
            }; 
        }

        private List<TransactionImportRow> GetTransactionImportRows()
        {
            return Rows?.Select(x => new TransactionImportRow() { Data = Newtonsoft.Json.JsonConvert.SerializeObject(x.GetProcessedTransaction()) })
                .ToList();
        }

        private List<TransactionImportEventLog> GetTransactionImportEventLogs()
        {
            return Errors?.Select(x => new TransactionImportEventLog() { CreatedDate = System.DateTime.Now, Message = x, Type = (byte)TransactionImportEventLogTypeEnum.Error })
                .ToList();
        }
    }
}