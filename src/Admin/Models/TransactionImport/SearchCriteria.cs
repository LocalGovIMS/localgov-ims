using System;
using Web.Mvc;

namespace Admin.Models.TransactionImport
{
    public class SearchCriteria
    {
        public int? TransactionImportTypeId { get; set; }

        public int? StatusId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public SelectList TransactionImportTypes { get; set; }
        public SelectList Statuses { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; } = 20;
    }
}