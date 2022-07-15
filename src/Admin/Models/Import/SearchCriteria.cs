using System;
using Web.Mvc;

namespace Admin.Models.Import
{
    public class SearchCriteria
    {
        public byte? DataType { get; set; }

        public int? ImportTypeId { get; set; }

        public int? StatusId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public SelectList DataTypes { get; set; }
        public SelectList ImportTypes { get; set; }
        public SelectList Statuses { get; set; }

        public int Page { get; set; }
        public int PageSize { get; set; } = 20;
    }
}