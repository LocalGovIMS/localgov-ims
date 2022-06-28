using System;

namespace BusinessLogic.Models.TransactionImport
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public int? TransactionImportTypeId { get; set; }

        public int? StatusId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}