using BusinessLogic.Enums;
using System;
using Web.Mvc;

namespace Admin.Models.EReturn
{
    public class SearchCriteria
    {
        public string Reference { get; set; }
        public decimal? Amount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? StatusId { get; set; }
        public int? TemplateId { get; set; }
        public EReturnType? EReturnType { get; set; }

        public int Page { get; set; }

        public SelectList Statuses { get; set; }
        public SelectList Templates { get; set; }
        public SelectList Types { get; set; }
    }
}