using BusinessLogic.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.UnitTests.Extensions.TransactionImport
{
    [TestClass]
    public class TestBase
    {
        protected const int TestUserId = 1;

        protected Entities.TransactionImport GetTransactionImport()
        {
            return new Entities.TransactionImport()
            {
                
            };
        }

        protected Entities.TransactionImport GetTransactionImportWithErrors()
        {
            var transactionImport = GetTransactionImport();

            transactionImport.EventLogs.Add(new Entities.TransactionImportEventLog()
            {
                Type = (byte)TransactionImportEventLogTypeEnum.Error
            });

            transactionImport.EventLogs.Add(new Entities.TransactionImportEventLog()
            {
                Type = (byte)TransactionImportEventLogTypeEnum.Error
            });

            return transactionImport;
        }

        protected Entities.TransactionImport GetTransactionImportWithInfo()
        {
            var transactionImport = GetTransactionImport();

            transactionImport.EventLogs.Add(new Entities.TransactionImportEventLog()
            {
                Type = (byte)TransactionImportEventLogTypeEnum.Info
            });

            transactionImport.EventLogs.Add(new Entities.TransactionImportEventLog()
            {
                Type = (byte)TransactionImportEventLogTypeEnum.Info
            });

            return transactionImport;
        }

        protected Entities.TransactionImport GetTransactionWithImportStatusHistories(TransactionImportStatusEnum latestStatus)
        {
            var transactionImport = GetTransactionImport();

            transactionImport.StatusHistories.Add(new Entities.TransactionImportStatusHistory()
            {
                CreatedDate = System.DateTime.Now.AddMinutes(-1),
                StatusId = (int)TransactionImportStatusEnum.Received
            });

            transactionImport.StatusHistories.Add(new Entities.TransactionImportStatusHistory()
            {
                CreatedDate = System.DateTime.Now,
                StatusId = (int)latestStatus
            });

            return transactionImport;
        }
    }
}
