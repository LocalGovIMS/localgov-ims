using BusinessLogic.Entities;
using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Extensions
{
    public static class TransactionImportExtensions
    {
        public static void Initialise(this TransactionImport item, int createdByUserId)
        {
            item.SetStatus(createdByUserId, TransactionImportStatusEnum.Received);
        }

        public static void Start(this TransactionImport item, int createdByUserId)
        {
            item.SetStatus(createdByUserId, TransactionImportStatusEnum.InProgress);
        }

        public static void Complete(this TransactionImport item, List<string> errors, int createdByUserId)
        {
            if(errors.Any())
            {
                errors.ForEach(x => item.AddError(x));
            }

            item.SetStatus(
                createdByUserId,
                item.HasErrors()
                    ? TransactionImportStatusEnum.Failed
                    : TransactionImportStatusEnum.Completed);
        }

        public static void Revert(this TransactionImport item, int createdByUserId)
        {
            item.SetStatus(createdByUserId, TransactionImportStatusEnum.Reverted);
        }

        private static void SetStatus(this TransactionImport item, int createdByUserId, TransactionImportStatusEnum status)
        {
            item.StatusHistories.Add(new TransactionImportStatusHistory()
            {
                CreatedDate = DateTime.Now,
                CreatedByUserId = createdByUserId,
                StatusId = (int)status,
            });
        }

        public static List<TransactionImportEventLog> Errors(this TransactionImport item)
        {
            return item.EventLogs?.Where(x => x.Type == (byte)TransactionImportEventLogTypeEnum.Error).ToList();
        }

        public static bool HasErrors(this TransactionImport item)
        {
            return !item.Errors().IsNullOrEmpty();
        }

        public static void AddError(this TransactionImport item, string error)
        {
            item.AddEventLog(error, TransactionImportEventLogTypeEnum.Error);
        }

        public static void AddInfo(this TransactionImport item, string message)
        {
            item.AddEventLog(message, TransactionImportEventLogTypeEnum.Info);
        }

        private static void AddEventLog(this TransactionImport item, string message, TransactionImportEventLogTypeEnum type)
        {
            if (item.EventLogs.IsNullOrEmpty())
                item.EventLogs = new List<TransactionImportEventLog>();

            item.EventLogs.Add(new TransactionImportEventLog()
            {
                CreatedDate = DateTime.Now,
                Type = (byte)type,
                Message = message
            });
        }
    }
}
