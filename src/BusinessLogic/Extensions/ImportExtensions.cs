using BusinessLogic.Entities;
using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLogic.Extensions
{
    public static class ImportExtensions
    {
        public static void Initialise(this Import item, int createdByUserId)
        {
            item.SetStatus(createdByUserId, ImportStatusEnum.Received);
        }

        public static void Start(this Import item, int createdByUserId)
        {
            item.SetStatus(createdByUserId, ImportStatusEnum.InProgress);
        }

        public static void Complete(this Import item, List<string> errors, int createdByUserId)
        {
            if(errors.Any())
            {
                errors.ForEach(x => item.AddError(x));
            }

            item.SetStatus(
                createdByUserId,
                item.HasErrors()
                    ? ImportStatusEnum.Failed
                    : ImportStatusEnum.Completed);
        }

        public static void Revert(this Import item, int createdByUserId)
        {
            item.SetStatus(createdByUserId, ImportStatusEnum.Reverted);
        }

        private static void SetStatus(this Import item, int createdByUserId, ImportStatusEnum status)
        {
            item.StatusHistories.Add(new ImportStatusHistory()
            {
                CreatedDate = DateTime.Now,
                CreatedByUserId = createdByUserId,
                StatusId = (int)status,
            });
        }

        public static List<ImportEventLog> Errors(this Import item)
        {
            return item.EventLogs?.Where(x => x.Type == (byte)ImportEventLogTypeEnum.Error).ToList();
        }

        public static bool HasErrors(this Import item)
        {
            return !item.Errors().IsNullOrEmpty();
        }

        public static void AddError(this Import item, string error)
        {
            item.AddEventLog(error, ImportEventLogTypeEnum.Error);
        }

        public static void AddInfo(this Import item, string message)
        {
            item.AddEventLog(message, ImportEventLogTypeEnum.Info);
        }

        private static void AddEventLog(this Import item, string message, ImportEventLogTypeEnum type)
        {
            if (item.EventLogs.IsNullOrEmpty())
                item.EventLogs = new List<ImportEventLog>();

            item.EventLogs.Add(new ImportEventLog()
            {
                CreatedDate = DateTime.Now,
                Type = (byte)type,
                Message = message
            });
        }

        public static ImportStatusEnum CurrentStatus(this Import item)
        {
            return (ImportStatusEnum)item.StatusHistories
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefault()
                .StatusId;
        }
    }
}
