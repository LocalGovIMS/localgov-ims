using System.ComponentModel.DataAnnotations;
using Web.Mvc;

namespace Admin.Models.FundMessage
{
    public class SearchCriteria
    {
        [Display(Name = "Fund code")]
        public string FundCode { get; set; }

        public string Message { get; set; }

        public int Page { get; set; }

        public SelectList Funds { get; set; }
    }
}