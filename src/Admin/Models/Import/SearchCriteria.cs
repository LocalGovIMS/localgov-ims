using System;
using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.Import
{
    public class SearchCriteria
    {
        [Display(Name = "Data type")]
        public byte? DataType { get; set; }

        [Display(Name = "Import type")]
        public int? ImportTypeId { get; set; }

        [Display(Name = "Current status")]
        public int? StatusId { get; set; }

        [Display(Name = "Start date")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "End date")]
        public DateTime? EndDate { get; set; }

        public SelectList DataTypes { get; set; }
        public SelectList ImportTypes { get; set; }
        public SelectList Statuses { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; } = 20;
    }
}