using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Admin.Models.Import
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        [DisplayName("Data type")]
        public ImportDataTypeEnum DataType { get; set; }

        [DisplayName("Type")]
        public string ImportTypeName { get; set; }

        [DisplayName("Created date")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Created by")]
        public string CreatedByUserName { get; set; }

        [DisplayName("Notes")]
        public string Notes { get; set; }

        [DisplayName("Number of rows")]
        public int NumberOfRows { get; set; }

        [DisplayName("Reversed date")]
        public DateTime? ReversedDate { get; set; }

        [DisplayName("Current status")]
        public ImportStatusEnum CurrentStatus { get; set; }

        public bool HasErrors { get; set; }

        public List<StatusHistoryViewModel> StatusHistories { get; set; }

        public List<EventLogViewModel> EventLogs { get; set; }
    }

    public class StatusHistoryViewModel
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserName { get; set; }
        public ImportStatusEnum Status { get; set; }
    }

    public class EventLogViewModel
    {
        public DateTime CreatedDate { get; set; }
        public ImportEventLogTypeEnum Type { get; set; }
        public string Message { get; set; }
    }
}