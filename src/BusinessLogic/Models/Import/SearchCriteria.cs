using System;

namespace BusinessLogic.Models.Import
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public byte? DataType { get; set; }

        public int? ImportTypeId { get; set; }

        public int? StatusId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}