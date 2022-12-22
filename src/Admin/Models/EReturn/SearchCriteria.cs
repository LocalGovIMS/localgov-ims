using BusinessLogic.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.EReturn
{
    public class SearchCriteria
    {
        public string Reference { get; set; }
        public decimal? Amount { get; set; }

        [Display(Name = "Start date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End date")]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Status")]
        public int? StatusId { get; set; }

        [Display(Name = "Template")]
        public int? TemplateId { get; set; }

        [Display(Name = "eReturn type")]
        public EReturnType? EReturnType { get; set; }

        public int Page { get; set; }

        public SelectList Statuses { get; set; }
        public SelectList Templates { get; set; }
        public SelectList Types { get; set; }
    }
}