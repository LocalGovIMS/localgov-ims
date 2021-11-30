using BusinessLogic.Enums;
using System;

namespace BusinessLogic.Models.EReturns
{
    public class SearchCriteria : BaseSearchCriteria
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Name { get; set; }
        public string EReturnNumber { get; set; }
        public decimal? Amount { get; set; }
        public int? StatusId { get; set; }
        public EReturnType? Type { get; set; }
        public int? TemplateId { get; set; }

        public SearchCriteria() : base()
        {
        }
    }
}