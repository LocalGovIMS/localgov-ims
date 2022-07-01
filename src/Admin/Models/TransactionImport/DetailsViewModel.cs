using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Admin.Models.TransactionImport
{
    public class DetailsViewModel
    {
        public int Id { get; set; }

        [DisplayName("Type")]
        public string TransactionImportTypeName { get; set; }

        [DisplayName("Created date")]
        public DateTime CreatedDate { get; set; }

        [DisplayName("Created by")]
        public string CreatedByUserName { get; set; }

        [DisplayName("External reference")]
        public string ExternalReference { get; set; }

        public string Description { get; set; }

        [DisplayName("Total amount")]
        public decimal TotalAmount { get; set; }

        [DisplayName("Number of rows")]
        public int TotalNumberOfTransactions { get; set; }

        [DisplayName("Reversed date")]
        public DateTime? ReversedDate { get; set; }

        [DisplayName("Current status")]
        public TransactionImportStatusEnum CurrentStatus { get; set; }

        public bool HasErrors { get; set; }

        public List<StatusHistoryViewModel> StatusHistories { get; set; }

        public List<EventLogViewModel> EventLogs { get; set; }
    }

    public class StatusHistoryViewModel
    {
        public DateTime CreatedDate { get; set; }
        public string CreatedByUserName { get; set; }
        public TransactionImportStatusEnum Status { get; set; }
    }

    public class EventLogViewModel
    {
        public DateTime CreatedDate { get; set; }
        public TransactionImportEventLogTypeEnum Type { get; set; }
        public string Message { get; set; }
    }
}